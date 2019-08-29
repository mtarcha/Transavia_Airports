select a.Iata, a.Lon, c.Name as Country, st.Name as Status, a.Name, con.Name as Continent, atype.TypeName as Type, a.Lat, si.SizeName as Size
from dbo.Airports as a 
	FULL OUTER JOIN dbo.Countries as c on a.CountryId = c.Id
	FULL OUTER JOIN dbo.Continents as con on c.ContinentId = con.Id
	FULL OUTER JOIN dbo.Sizes as si on a.SizeId = si.Id
	FULL OUTER JOIN dbo.Statuses as st on a.StatusId = st.Id
	FULL OUTER JOIN dbo.AirportTypes as atype on a.TypeId = atype.Id

