from collections import defaultdict
from markdown.extensions.toc import slugify

def raiseInvalidValueType():
    raise ValueError(f"Unrecognized value type {valueType}")

typeValuesDict = defaultdict(
    raiseInvalidValueType,
    {
        'bool': '`#!js true` or `#!js false`',
        'int32': 'Any number between `#!json -2147483648` and `#!json 2147483647`',
        'string': 'Any piece of text or string',

        'ConnectionTimeEnum': '''

- `Initialization` - connect to Redis during plugin initialization (`TerrariaPlugin.Initialize()`).
- `ServerStarted` - connect to Redis after the server starts listening for connections.''',
    }
)

def define_env(env):
    @env.macro
    def configOptionValues(valueType, defaultValue, allValues=None):
        result = f'**Default Value**: `#!json {defaultValue}`<br>'

        if allValues is None:
            allValues = typeValuesDict[valueType]

        if allValues:
            result += f'**All Values**: {allValues}'

        return result

    @env.macro
    def command(name, description, names=None, permissions=None):
        preserve_unicode = False

        mdx_configs = env.conf.get('mdx_configs')
        slugify_f = slugify
        separator = '-'
        if 'toc' in env.conf['markdown_extensions'] and mdx_configs:
            toc_config = mdx_configs.get('toc')
            if toc_config:
                preserve_unicode = toc_config.get('unicode') or preserve_unicode
                slugify_f = toc_config.get('slugify') or slugify_f
                separator = toc_config.get('separator') or separator

        slugify_s = lambda value: slugify_f(value, separator, preserve_unicode)

        result = f'## {name}\n{description}'

        if names:
            result += f'\n\n### Names {{#{slugify_s(f"{name}-names")}}}'

            names.sort(key=len)
            for name in names:
                result += f'\n- `{name}`'
        else:
            raise ValueError(f'Command "{name}" has no names')

        result += f'\n\n### Permissions {{#{slugify_s(f"{name}-permissions")}}}'

        if permissions:
            permissions.sort(key=len)
            for permission in permissions:
                result += f'\n- `{permission}`'
        else:
            result += '\nNo permissions required'

        return result
