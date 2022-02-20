# General
Specifies the general plugin configuration.

## Path
`{SAVE_PATH}/config.json`

## Options

### `showCredits`
**Default value**: `#!json true`<br>
**All values**:
--8<-- "includes/Configuration/bool.md"

Whether or not to show credits to dependencies at start up.

The credits(`/helpopplugin:credits`) command will function regardless of this option.

### `reportOrigin`
**Default value**: `#!json null`<br>
**All values**:
--8<-- "includes/Configuration/string-any.md"

The current server's name when sending reports.

!!! example

    Setting this option to "`Server1`" will cause reports originating from the current server to be tagged with "`Server1`". Templates may pick up on it and display it like so: `[Report] from Server1: ...`.

### `reportMessageColor`
**Default value**: `#!json 14393897`(in hex: `DBA229`)<br>
**All values**:
--8<-- "includes/Configuration/int32.md"

The display color of report messages. This option only applies for the current server, other servers may have different display colors.

!!! note

    The Alpha value/channel is always ignored.

### `useTShockReload`
**Default value**: `#!json true`<br>
**All values**:
--8<-- "includes/Configuration/bool.md"

Whether or not to listen to TShock's Reload hook, also known as `/reload`.
