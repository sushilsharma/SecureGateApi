CREATE PROCEDURE GetCustomerByMnemonic
	  @xmlDoc XML
AS
BEGIN

SET NOCOUNT ON;

    DECLARE @intPointer INT;
    DECLARE @CustomerMnemonic NVARCHAR(200);

    -- Prepare XML document
    EXEC sp_xml_preparedocument @intPointer OUTPUT, @xmlDoc;

    -- Extract parameters from XML using OPENXML
    SELECT 
        @CustomerMnemonic = tmp.CustomerMnemonic
    FROM OPENXML(@intPointer, '/Json', 2)
    WITH (
        CustomerMnemonic NVARCHAR(200)
    ) tmp;

    -- Clean up XML document
    EXEC sp_xml_removedocument @intPointer;

    -- Select the customer record based on the provided CustomerMnemonic
      ;WITH XMLNAMESPACES('http://james.newtonking.com/projects/json' AS json)
    SELECT 
        CAST((SELECT 
            'true' AS [@json:Array],
        CustomerId,
        CustomerName,
        CustomerMnemonic,
        CustomerType,
        ParentCustomer,
        AddressLine1,
        AddressLine2,
        AddressLine3,
        City,
        State,
        CountryId,
        Country,
        Postcode,
        Region,
        ZoneCode,
        CategoryCode,
        Email,
        SiteURL,
        ContactPersonNumber,
        ContactPersonName,
        logo,
        header,
        footer,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        IsActive,
        Field2,
        Field3,
        Field4,
        Field5,
        Field6,
        Field7,
        Field8,
        Field9,
        Field10,
        PaymentTermCode,
        CategoryType,
        DistrictName,
        CountryCode,
        CityName,
        TanentCode,
        PaymentTerm,
        Priority
    FROM [dbo].[Customer]
    WHERE CustomerMnemonic = @CustomerMnemonic
	   FOR XML PATH('Customer'), ELEMENTS, ROOT('Customers')) AS XML) AS Result;
END