from collections import defaultdict

class ConfigOption:
    def __init__(self, valueType, defaultValue, allValues=None):
        self.valueType = valueType
        self.defaultValue = defaultValue
        self.allValues = allValues if allValues != None else typeValuesDict[valueType]

    def to_markdown(self):
        result = f"**Default Value**: `#!json {self.defaultValue}`<br>"

        if self.allValues:
            result += f"**All Values**: {self.allValues}"

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
        return ConfigOption(valueType, defaultValue, allValues).to_markdown()
