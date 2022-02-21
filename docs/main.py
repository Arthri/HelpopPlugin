from collections import defaultdict

def configOptionTemplate(valueType, defaultValue, allValues=None):
        result = f"**Default Value**: `#!json {defaultValue}`<br>"

        if allValues == None:
            allValues = typeValuesDict[valueType]

        if allValues:
            result += f"**All Values**: {allValues}"

        return result

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
        return configOptionTemplate(valueType, defaultValue, allValues)
