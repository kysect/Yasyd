# Yasyd

Components:

- Communication protocol
  - Certificates
- Server and Client
  - request handler registration API
  - Validator for checking API registration
  - Version check
  - Command registration over controllers
- Monitoring
  - Performance metrics
- Database
  - Scaling
  - Replications
  - Master/slave interaction
  - sharding
- Gateway
  - Load balancer
  - DoS protection
  - Distributed gateways
- Queue
  - Master and clients
- Cache
  - Key-value storages
  - Db cache
  - session
- Instance manager
  - Auto scaling

## Artifact diagrams

> TODO: add instance manager for autoscaling

```puml

package ClientSide
{
  object Client
}

package DataAccess
{
  object Cache
  object SessionStorage
  object Database
}

package Server
{
  object TrafficLimiter
  object Gateway
  object InstanceDiscoveryService
  object MessageQueue
  object Workers

  Client --> TrafficLimiter
  TrafficLimiter --> Gateway
  Gateway --> Workers
  Gateway --> InstanceDiscoveryService
  Workers --> InstanceDiscoveryService
  MessageQueue --> Workers
  Workers --> MessageQueue
  
  Workers --> Cache
  Workers --> SessionStorage
  Workers --> Database
}

package Notifications
{
  object NotificationQueue
  object NotificationWorker

  Workers --> NotificationQueue
  NotificationQueue --> NotificationWorker
  NotificationWorker --> Client
  NotificationWorker --> Database
}

package Metrics
{
  object LogStorage
  object MetricStorage
  object Monitoring

  Gateway --> MetricStorage
  Workers --> LogStorage
  Workers --> MetricStorage
  Monitoring --> LogStorage
  Monitoring --> MetricStorage
  Monitoring --> InstanceDiscoveryService
}

```

## Artifact description

### TrafficLimiter

TrafficLimiter предоставляет возможность установить лимит на количество запросов, которые могут приходить на обработку. Виды лимитов:

- Лимиты на количество запросов в целом
- Лимиты на количество запросов от одного пользователя

Виды лимитирования:

- Лимит на количество за интервал времени
- Лимит на количество активных обрабатываемых запросов

### Gateway

Gateway - это нода, которая отвечает за роутинг и балансировку нагрузки. Гейтвей перенаправляет запросы на нужные вычислительные узлы.

### Instance discovery service

Instance discovery service - сервис, который мониторит состояние всех работающих сервисов, проверять, что они живы. Также он предоставляет интерфейс для того, чтобы новые сервисы сообщали о своём запуски, а другие (гейтвей) получали актуальную информацию о доступных нодах.

### Message queue

Очередь сообщений предоставляет механизм асинхронного взаимодействия между сервисами. Сервисы могут писать сообщения в поток, также могут подписываться на обработку сообщений.

### Monitoring

Мониторинг за инфраструктурой заключается в агрегации метрик, получаемых от сервисов. Сервисы во время работы передают метрики в главную ноду.

### Communication

Для взаимодействия между нодами используется собственный протокол. Состав запрос должен выглядеть так:

> TODO: Скорее всего, тут нужно будет вводить две разные меты. Одна для системы - например, ид сессии и идентификатор пользователя. Замена хедеров хттп. А вторая - пользовательская

- Тип запроса. Типы нужны для отделения служебных от пользовательских
- Размер системной метаинформации
- Системная метаинформация
- Размер метаинформации
- Метаинформация
- Размер основного тела
- Основное тело

Например, для пользовательского запроса пакет может выглядеть так:

- Тип запроса = 0
- Размер системной метаинформации - это количество передаваемых хедеров
- Системная метаинформация - набор пар, которые описывают метаинфомрацию. Пишутся через размер ключа, ключ, размер значения, значение.
- Размера метаинформации - это длинна строки, которая идентифицирует что именно за команда пришла
- Метаинформация - это данные, которые описывают команду. Например, это может быть url, если мы мигрируем с HTTP
- Размер основного тела - длинна сериализованной строки с телом запроса
- Основное тело - сериализованное тело строки