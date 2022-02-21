# General
Specifies the general plugin configuration.

## Path
`{SAVE_PATH}/config.json`

## Options

### `showCredits`
{{ configOptionValues("bool", "true") }}

Whether or not to show credits to dependencies at start up.

The credits(`/helpopplugin:credits`) command will function regardless of this option.

### `reportOrigin`
{{ configOptionValues("string", "null") }}

The current server's name when sending reports.

!!! example

    Setting this option to "`Server1`" will cause reports originating from the current server to be tagged with "`Server1`". Templates may pick up on it and display it like so: `[Report] from Server1: ...`.

### `reportMessageColor`
{{ configOptionValues("int32", "14393897") }}

The display color of report messages. This option only applies for the current server, other servers may have different display colors.

!!! note

    The Alpha value/channel is always ignored.

### `useTShockReload`
{{ configOptionValues("bool", "true") }}

Whether or not to listen to TShock's Reload hook, also known as `/reload`.
