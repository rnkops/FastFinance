# FastFinance

Double-entry accounting made easier.

[![Dotnet](https://img.shields.io/badge/platform-.NET-blue)](https://www.nuget.org/packages/FastFinance/)
[![License](https://img.shields.io/github/license/rnkops/FastFinance)](LICENSE)

FastFinance is a .NET library for double-entry accounting. It is designed to be easy to use and to provide a simple API for common accounting tasks.

## Warning ⚠️

Version 2 is still in pre-release and is not yet ready for production use. It is recommended to use version 1 until version 2 is released.

Version 2 is an overhaul of the library and contains breaking changes.

Most notably, there are interfaces for `Account`, `Transaction`, `ChartOfAccounts` and `Journal` that can be used to implement custom logic and generic usage.

`ChartOfAccount` was renamed to `ChartOfAccounts` to be consistent with the other interfaces.

`Journal` is now an abstract class

## Installation

In your terminal, run:

```bash
dotnet add package FastFinance
```

To install the `FastCrud` EntityFrameworkCore adapter, run:

```bash
dotnet add package FastFinance.FastCrud.EFCore
```

## Overview

FastFinance is a double-entry accounting library for .NET. It is designed to be easy to use and easy to extend.

### Concepts

FastFinance is built around the following concepts:

- **Account**:
  - A financial account, such as a bank account, a cash account, or a liability account.
  - Accounts can be created and deleted at any time.
  - Accounts can be assigned to a parent account, which is used to group accounts together.
  - Accounts can have multiple balances, which are used to track different currencies.
  
- **Transaction**:

  - A transaction is an operation that changes the balance of two or more accounts.
  - A transaction has to be balanced, meaning that the sum of debits must equal the sum of credits.
  - The base transaction provided by FastFinance is the most crude one, and it's possible to extend it to add more information while still keeping the transaction balanced.

- **Journal**:
  - A journal is a collection of transactions.
  - A jounral is what is used to track the history of an account.
  
- **Chart of Accounts**:
  - A chart of accounts is a collection of accounts.

## Documentation

Coming soon.

In the meantime, you can check out some [examples](/examples/)

## Contributing

Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

## License

[MIT](LICENSE)

Copyright (c) 2022 rnkops
