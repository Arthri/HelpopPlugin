# Redis
Specifies the configuration for the Redis connection.

## Path
`{SAVE_PATH}/config-redis.json`

## Options

### `configString`
{{ configOptionValues("string", "localhost", "") }}

The string containing the Redis configuration. The documentation for the string is available [here](https://stackexchange.github.io/StackExchange.Redis/Configuration).

### `connectionTime`
{{ configOptionValues("ConnectionTimeEnum", "Initialization") }}

When the Redis connection will be initiated.
