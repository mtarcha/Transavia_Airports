using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Transavia.Application.Queries.GetAirports;

namespace Transavia.Application.Queries.Sql
{
    public class GetAirportsQueryHandler : IGetAirportsQueryHandler
    {
        private readonly IConnectionFactory _connectionFactory;

        public GetAirportsQueryHandler(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<GetAirportsResult> Handle(GetAirportsQuery request, CancellationToken cancellationToken)
        {
            using (var connection = _connectionFactory.Create())
            {
                //var query = @"Select b.Id, b.Name, b.Picture, b.Date, b.Summary, b.Rate, 
                //                    a.Id, a.Name as FirstName, a.SurName as LastName, a.DateOfBirth, a.DateOfDeath
                //            from dbo.BookAuthorEntity as ba 
                //                inner join dbo.Books as b on ba.BookId = b.Id 
	               //             inner join dbo.Authors as a on ba.AuthorId = a.Id
                //            where b.Id in (Select Id from 
                //                (Select distinct b.Id, b.Rate
                //                from dbo.BookAuthorEntity as ba 
                //                    inner join dbo.Books as b on ba.BookId = b.Id 	
                //                where b.Name like @SearchPattern or b.Summary like @SearchPattern
                //                ORDER BY b.Rate DESC 
                //                OFFSET @SkipCount ROWS 
                //                FETCH NEXT @TakeCount ROWS ONLY ) as books)";

                //var books = new Dictionary<Guid, Book>();
                //var res = await connection.QueryAsync<Book, Author, Book>(
                //    query,
                //    (book, author) =>
                //    {
                //        if (!books.TryGetValue(book.Id, out var theBook))
                //        {
                //            theBook = book;
                //            books.Add(book.Id, theBook);
                //        }

                //        theBook.Authors.Add(author);
                //        return theBook;
                //    },
                //    new { SearchPattern = $"%{request.SearchPattern}%", SkipCount = request.SkipCount, TakeCount = request.TakeCount });

                //var totalBooksCount = await connection.ExecuteScalarAsync<int>(
                //    @"Select Count(*) 
                //        from dbo.Books 
                //        where Name like @SearchPattern or Summary like @SearchPattern",
                //    new { SearchPattern = $"%{request.SearchPattern}%" });

                //return new SearchBooksResult(books.Values, totalBooksCount, books.Count);

                string getAirportsQuery = null;
                string getAirportsTotalCountQuery = null;
                if (request.CountryId == null)
                {
                    getAirportsQuery = @"select a.Iata, a.Lon, a.Lat, a.Name, st.Name as Status,
                                                c.Name as Country, con.Name as Continent, atype.TypeName as Type, si.SizeName as Size
                                        from dbo.Airports as a 
	                                        FULL OUTER JOIN dbo.Countries as c on a.CountryId = c.Id
	                                        FULL OUTER JOIN dbo.Continents as con on c.ContinentId = con.Id
	                                        FULL OUTER JOIN dbo.Sizes as si on a.SizeId = si.Id
	                                        FULL OUTER JOIN dbo.Statuses as st on a.StatusId = st.Id
	                                        FULL OUTER JOIN dbo.AirportTypes as atype on a.TypeId = atype.Id
                                        order by a.Name
                                        OFFSET @SkipCount ROWS
                                        FETCH NEXT @TakeCount ROWS ONLY";

                    getAirportsTotalCountQuery = "Select Count(*) from dbo.Airports";
                }
                else
                {
                    getAirportsQuery = @"select a.Iata, a.Lon, a.Lat, a.Name, st.Name as Status, 
                                                c.Name as Country, con.Name as Continent, atype.TypeName as Type, si.SizeName as Size
                                        from dbo.Airports as a 
	                                        FULL OUTER JOIN dbo.Countries as c on a.CountryId = c.Id
	                                        FULL OUTER JOIN dbo.Continents as con on c.ContinentId = con.Id
	                                        FULL OUTER JOIN dbo.Sizes as si on a.SizeId = si.Id
	                                        FULL OUTER JOIN dbo.Statuses as st on a.StatusId = st.Id
	                                        FULL OUTER JOIN dbo.AirportTypes as atype on a.TypeId = atype.Id
                                        where a.Id in (select a.Id
											from dbo.Airports as a inner join dbo.Countries as c on a.CountryId = c.Id
											where c.Id = @Country
                                            order by a.Name
                                            OFFSET @SkipCount ROWS
                                            FETCH NEXT @TakeCount ROWS ONLY)";

                    getAirportsTotalCountQuery = @"Select Count(*) 
                                                    from dbo.Airports as a inner join dbo.Countries as c on a.CountryId = c.Id 
                                                    where c.Id = @Country";
                } 
                
                var airports = await connection.QueryAsync<Airport>(getAirportsQuery, new { request.SkipCount, request.TakeCount, Country = request.CountryId});
                var totalBooksCount = await connection.ExecuteScalarAsync<int>(getAirportsTotalCountQuery, new { Country = request.CountryId});
                
                return new GetAirportsResult
                {
                    TotalFound = totalBooksCount,
                    Airports = airports
                };
            }
        }
    }
}