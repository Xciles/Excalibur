using System;
using System.Collections.Generic;
using System.Text;

namespace Excalibur.AspNetCore.ViewModels
{
    public class SearchResult<T>
    {
        public int TotalEntries { get; set; }
        public int TotalResults { get; set; }
        public IList<T> Results { get; set; }

        public SearchResult()
        {

        }
        public SearchResult(IList<T> results, int totalResults)
        {
            Results = results;
            TotalResults = totalResults;
        }

        public SearchResult(IList<T> results, int totalResults, int totalEntries)
        {
            Results = results;
            TotalResults = totalResults;
            TotalEntries = totalEntries;
        }
    }
}
