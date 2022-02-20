# Report Template
Specifies the template to use when formatting reports.

---

## Path
`{SAVE_PATH}/report-template.scriban-txt`

---

## Default Value
```liquid
--8<-- "src/HelpopPlugin/Configuration/ReportTemplate.scriban-txt"
```

---

## Template Syntax
The underlying templating engine is [Scriban]. It has a very similar syntax to Liquid templates and C#. Its language guide is available [here][Scriban-Syntax].

---

## Model
Data available exposed to the template.

---

### `issue`
`#!csharp Issue`. The raised report.

---

### `issue.message`
`#!csharp string`. The report's contents.

---

### `issue.issuer`
`#!csharp IssueUser`. The issuer's information.

---

### `issue.issuer.name`
`#!csharp string`. The issuer's display name.

---

### `issue.issuer.ip`
`#!csharp string`. The issuer's IP address.

---

### `issue.issuer.uuid`
`#!csharp string`. The issuer's UUID.

??? note "Hashed UUIDs from TShock servers"

    If the issue originated from a TShock server, the UUID is hashed using SHA512 for security reasons. For example(randomly generated UUIDs):

    - `00000000-0000-0000-0000-000000000000` --> `08c1bde74a3faf09b41c2916c5c041458b584f289aceabca4d504a69ce6020f51ff3d36d062a5c67a4dfd924ae923b5d4a892b4ab5c8977db5f2f6585e70d069`
    - `bb13f559-b631-2d03-88f8-736a4491f6dc` --> `34ed6ccd10673d5f19365e695c6bd939aa029335d0195ccb0d04437c3d3652c3740991f217762ad965a6e11bae334b9a6a77ecfa13af93008a4e06fe5d90104a`
    - `3017f25c-ad62-8d2a-58c7-3b1106238a38` --> `a7ca862754c44c5c708a400f0440cc7cd46bcce99bb98eb74e76f16030df21bb61e570c723545ee90559a0a62ae8a762ec369d420d70a9ff87091d652216bf41`

    Reference: [this](https://github.com/Pryaxis/TSAPI/blob/4ac9528825390ac3a15f096b248fcda33f5d210f/TerrariaServerAPI/TerrariaApi.Server/HookManager.cs#L477)

---

### `issue.issuer.account`
`#!csharp UserAccount`. The issuer's account.

---

### `issue.issuer.account.id`
`#!csharp int`. The issuer's account ID.

---

### `issue.issuer.account.name`
`#!csharp string`. The issuer's account name.

---

### `issue.origin`
`#!csharp string`. The report's origin server.

<!--


Index


-->
[Scriban]: https://github.com/scriban/scriban
[Scriban-Syntax]: https://github.com/scriban/scriban/blob/master/doc/language.md
