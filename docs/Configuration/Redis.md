# Redis
Specifies the configuration for the Redis connection.

## Path
`{SAVE_PATH}/config-redis.json`

## Options

### `configString`
**Default value**: `#!json "localhost"`

The string containing the Redis configuration. The documentation for the string is available [here](https://stackexchange.github.io/StackExchange.Redis/Configuration).

### `connectionTime`
**Default value**: `#!json 0`(`#!json "Initialization"`)<br>
**All values**:

--8<-- "includes/Configuration/ConnectionTimeEnum.md"

When the Redis connection will be initiated.
