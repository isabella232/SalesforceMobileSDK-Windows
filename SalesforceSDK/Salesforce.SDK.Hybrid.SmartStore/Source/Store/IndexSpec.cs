﻿using System.Collections.ObjectModel;
using System.Runtime.InteropServices.WindowsRuntime;
using Newtonsoft.Json;
using Salesforce.SDK.Auth;
using Salesforce.SDK.SmartStore.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salesforce.SDK.Hybrid.SmartStore
{
    public sealed class IndexSpec
    {
        private SDK.SmartStore.Store.IndexSpec _indexSpec;

        public IndexSpec()
        {
            throw new InvalidOperationException("IndexSpec without parameters is not supported");
        }

        public IndexSpec(String path, SmartStoreType type)
        {
            _indexSpec = new SDK.SmartStore.Store.IndexSpec(path,
                new SDK.SmartStore.Store.SmartStoreType(type.ColumnType));
        }

        public IndexSpec(String path, SmartStoreType type, String columnName)
        {
            _indexSpec = new SDK.SmartStore.Store.IndexSpec(path,
                new SDK.SmartStore.Store.SmartStoreType(type.ColumnType), columnName);
        }

        public static IDictionary<string, IndexSpec> MapForIndexSpecs([ReadOnlyArray()]IndexSpec[] indexSpecs)
        {
            return indexSpecs.ToDictionary(indexspecs => indexspecs._indexSpec.Path);
        }

        internal static SDK.SmartStore.Store.IndexSpec[] ConvertToSdkIndexSpecs(IndexSpec[] indexSpecs)
        {
            var specs = from n in indexSpecs select n._indexSpec;
            return specs.ToArray();
        }

        internal static IndexSpec[] ConvertToHybridIndexSpecs(SDK.SmartStore.Store.IndexSpec[] indexSpecs)
        {
            var specs = JsonConvert.SerializeObject(indexSpecs);
            return JsonConvert.DeserializeObject<IndexSpec[]>(specs);
        }
    }
}