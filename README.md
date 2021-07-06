# NServiceBus.CustomChecks.ElasticSearch

[![Build status](https://ci.appveyor.com/api/projects/status/m9cejtr15ikcmc8u/branch/master?svg=true)](https://ci.appveyor.com/project/TraGicCode/nservicebus-customchecks-elasticsearch)
[![Nuget](https://img.shields.io/nuget/v/NServiceBus.CustomChecks.ElasticSearch/)](https://www.nuget.org/packages/NServiceBus.CustomChecks.ElasticSearch)
[![Nuget downloads](https://img.shields.io/nuget/dt/NServiceBus.CustomChecks.ElasticSearch/)](https://www.nuget.org/packages/NServiceBus.CustomChecks.ElasticSearch)
[![License](https://img.shields.io/github/license/TraGicCode/NServiceBus.CustomChecks.ElasticSearch.svg)](https://github.com/TraGicCode/NServiceBus.CustomChecks.ElasticSearch/blob/master/LICENSE)

#### Table of Contents

1. [Description](#description)
1. [How to use it](#how-to-use-it)
1. [How it works](#how-it-works)
1. [Development - Guide for contributing to the module](#contributing)

## Description

A Reusable ElasticSearch NServiceBus CustomCheck to check the availability and connectivity to the database.

## How to use it

In order to begin using this custom check simply create a concrete for each ElasticSearch remote you would like to perform a healthcheck on.

```c#
    namespace Ordering.Endpoint.CustomChecks
    {
        public class OrderingElasticSearchCustomCheck : ElasticSearchCustomCheck
        {
            private static TimeSpan repeat = TimeSpan.FromSeconds(5);
            private static Uri elasticSearchUri = new Uri("http://localhost:9200");
            public OrderingElasticSearchCustomCheck(): base(elasticSearchUri, repeat)
            {

            }
        }
    }
```

## Contributing

1. Fork it ( <https://github.com/tragiccode/NServiceBus.CustomChecks.ElasticSearch/fork> )
1. Create your feature branch (`git checkout -b my-new-feature`)
1. Commit your changes (`git commit -am 'Add some feature'`)
1. Push to the branch (`git push origin my-new-feature`)
1. Create a new Pull Request