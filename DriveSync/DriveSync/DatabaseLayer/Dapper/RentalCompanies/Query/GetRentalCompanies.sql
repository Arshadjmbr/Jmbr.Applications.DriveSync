SELECT 
	COUNT(*) OVER() AS TotalRecords,
	"Id",
	"CompanyName",
	"ContactNumber",
	"Address",
	"Status",
	"CreatedDateTime",
	"UpdatedDateTime",
	"Remarks",
	"Deleted"
FROM RentalCompanies
WHERE
  @searchText IS NULL OR
  CompanyName LIKE '%' + @searchText + '%'
ORDER BY
"CreatedDateTime" DESC
OFFSET @rowSkip ROWS 
FETCH NEXT @takeRows ROWS ONLY;