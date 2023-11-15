﻿using Oracle.ManagedDataAccess.Client;
using PEMS.Models;
using System.Data;

namespace PEMS.Contracts
{
    public interface IOracleDataAccessProvider
    {
        List<PEMSystem> GetItems(string query);

        void AddItem(string query, OracleParameter[] parameters);

        T GetItem<T>(string query, object obj, bool isStoredProcedure = false);

        bool UpdateItem(string query, OracleParameter[] parameters);
    }
}
