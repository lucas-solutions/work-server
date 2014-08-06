﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Lucas.Solutions.Diagnostics
{
    using Lucas.Solutions.Diagnostics.Responses;

    public class Searcher : ISearcher, IRequestContext
    {
        private const string _domain = ".loggly.com/";
        private readonly string _url;

        public Searcher(string subdomain)
        {
            _url = string.Concat(subdomain, _domain);
        }

        public string Url
        {
            get { return _url; }
        }

        public SearchResponse Search(string query)
        {
            return Search(new SearchQuery { Query = query });
        }

        public SearchResponse Search(string query, DateTime start, DateTime until)
        {
            return Search(new SearchQuery { Query = query, From = start, Until = until });
        }

        public SearchResponse Search(string query, int startingAt, int numberOfRows)
        {
            return Search(new SearchQuery { Query = query, StartingAt = startingAt, NumberOfRows = numberOfRows });
        }

        public SearchResponse Search(string query, DateTime start, DateTime until, int startingAt, int numberOfRows)
        {
            return Search(new SearchQuery { Query = query, From = start, Until = until, StartingAt = startingAt, NumberOfRows = numberOfRows });
        }

        public SearchResponse Search(SearchQuery query)
        {
            var communicator = new LogglyHttpCommunicator(this);
            return communicator.GetPayload<SearchResponse>("api/search", query.ToParameters());
        }

        public SearchJsonResponse SearchJson(string query)
        {
            return SearchJson(new SearchQuery { Query = query });
        }

        public SearchJsonResponse SearchJson(string query, DateTime start, DateTime until)
        {
            return SearchJson(new SearchQuery { Query = query, From = start, Until = until });
        }

        public SearchJsonResponse SearchJson(string query, int startingAt, int numberOfRows)
        {
            return SearchJson(new SearchQuery { Query = query, StartingAt = startingAt, NumberOfRows = numberOfRows });
        }

        public SearchJsonResponse SearchJson(string query, DateTime start, DateTime until, int startingAt, int numberOfRows)
        {
            return SearchJson(new SearchQuery { Query = query, From = start, Until = until, StartingAt = startingAt, NumberOfRows = numberOfRows });
        }

        public SearchJsonResponse SearchJson(string property, string value)
        {
            var query = GetJsonQuery(new Dictionary<string, string> { { property, value } });
            return SearchJson(new SearchQuery { Query = query });
        }

        public SearchJsonResponse SearchJson(string property, string value, DateTime start, DateTime until)
        {
            var query = GetJsonQuery(new Dictionary<string, string> { { property, value } });
            return SearchJson(new SearchQuery { Query = query, From = start, Until = until });
        }

        public SearchJsonResponse SearchJson(string property, string value, int startingAt, int numberOfRows)
        {
            var query = GetJsonQuery(new Dictionary<string, string> { { property, value } });
            return SearchJson(new SearchQuery { Query = query, StartingAt = startingAt, NumberOfRows = numberOfRows });
        }

        public SearchJsonResponse SearchJson(string property, string value, DateTime start, DateTime until, int startingAt, int numberOfRows)
        {
            var query = GetJsonQuery(new Dictionary<string, string> { { property, value } });
            return SearchJson(new SearchQuery { Query = query, From = start, Until = until, StartingAt = startingAt, NumberOfRows = numberOfRows });
        }

        public SearchJsonResponse SearchJson(SearchQuery query)
        {
            var communicator = new LogglyHttpCommunicator(this);
            return communicator.GetPayload<SearchJsonResponse>("api/search", query.ToParameters());
        }

        private static string GetJsonQuery(IEnumerable<KeyValuePair<string, string>> properties)
        {
            var sb = new StringBuilder();
            foreach (var prop in properties)
            {
                sb.AppendFormat("json.{0}:{1} ", prop.Key, prop.Value);
            }
            return sb.ToString();
        }
    }
}