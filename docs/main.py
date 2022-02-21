from collections import defaultdict

def raiseInvalidValueType():
    raise ValueError(f"Unrecognized value type {valueType}")

typeValuesDict = defaultdict(
    raiseInvalidValueType,
    {
        'bool': '`#!js true` or `#!js false`',
        'int32': 'Any number between `#!json -2147483648` and `#!json 2147483647`',
        'string': 'Any piece of text or string',

        'ConnectionTimeEnum': """

- `Initialization` - connect to Redis during plugin initialization (`TerrariaPlugin.Initialize()`).
- `ServerStarted` - connect to Redis after the server starts listening for connections.""",
    }
)

def define_env(env):
    @env.macro
    def configOptionValues(valueType, defaultValue, allValues=None):
        result = f"**Default Value**: `#!json {defaultValue}`<br>"

        if allValues == None:
            allValues = typeValuesDict[valueType]

        if allValues:
            result += f"**All Values**: {allValues}"

        return result

    @env.macro
    def command(name, description, aliases=None, permissions=None):
        result = f"""## {name}
{description}"""

        if aliases:
            result += """

### Aliases"""

            aliases.sort()
            for alias in aliases:
                result += f"\n- `{alias}`"

        result += """

### Permissions"""

        if permissions:
            permissions.sort()
            for permission in permissions:
                result += f"\n- `{permission}`"
        else:
            result += "\nNo permissions required"

        return result
