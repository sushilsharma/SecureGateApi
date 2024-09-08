CREATE PROCEDURE [dbo].[ISP_Customer] --'<Customers>   <Customer>  <CustomerId>17</CustomerId>   <CustomerName>Jane Doe 12</CustomerName>     <CustomerMnemonic>SOC244557</CustomerMnemonic>     <CustomerType>Wholesale</CustomerType>     <ParentCustomer></ParentCustomer>     <AddressLine1>123 Elm Street</AddressLine1>     <AddressLine2>Suite 456</AddressLine2>     <AddressLine3></AddressLine3>     <City>Springfield</City>     <State>IL</State>     <CountryId>1</CountryId>     <Country>USA</Country>     <Postcode>62701</Postcode>     <Region>Midwest</Region>     <ZoneCode>MW01</ZoneCode>     <CategoryCode>CATEGORY01</CategoryCode>     <Email>janedoe@example.com</Email>     <SiteURL>http://example.com</SiteURL>     <ContactPersonNumber>555-1234</ContactPersonNumber>     <ContactPersonName>Jane Doe</ContactPersonName>     <logo></logo>     <header></header>     <footer></footer>     <CreatedBy>2</CreatedBy>     <CreatedDate>2024-09-07T00:00:00</CreatedDate>     <IsActive>1</IsActive>   </Customer> </Customers>'
    @XMLData XML
AS
BEGIN
    -- Declare a handle for the XML document
    DECLARE @DocHandle INT;
	DECLARE @Year NVARCHAR(2);
    DECLARE @RandomNumber NVARCHAR(4);
    DECLARE @CustomerMnemonic NVARCHAR(60);
	DECLARE @CustomerId bigint
    -- Get the last two digits of the current year
    SET @Year = RIGHT(YEAR(GETDATE()), 2);

    -- Generate a 4-digit random number
    SET @RandomNumber = RIGHT('0000' + CAST(ABS(CHECKSUM(NEWID())) % 10000 AS NVARCHAR(4)), 4);

    -- Concatenate year and random number
    SET @CustomerMnemonic = 'SOC'+@Year + @RandomNumber;

    -- Create an XML document handle
    EXEC sp_xml_preparedocument @DocHandle OUTPUT, @XMLData;

    -- Insert data from the XML document into the Customer table
    INSERT INTO [dbo].[Customer] 
    (
        [CustomerName],
        [CustomerMnemonic],
        [CustomerType],
        [ParentCustomer],
        [AddressLine1],
        [AddressLine2],
        [AddressLine3],
        [City],
        [State],
        [CountryId],
        [Country],
        [Postcode],
        [Region],
        [ZoneCode],
        [CategoryCode],
        [Email],
        [SiteURL],
        [ContactPersonNumber],
        [ContactPersonName],
        [logo],
        [header],
        [footer],
        [CreatedBy],
        [CreatedDate],
        [IsActive]
    )
    SELECT 
        CustomerName,
        @CustomerMnemonic,
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
        (select dbo.GetDateLocal()),
        IsActive
    FROM OPENXML(@DocHandle, '/Customers/Customer', 2)
    WITH (
        CustomerName NVARCHAR(500),
        CustomerMnemonic NVARCHAR(200),
        CustomerType NVARCHAR(50),
        ParentCustomer BIGINT,
        AddressLine1 NVARCHAR(100),
        AddressLine2 NVARCHAR(100),
        AddressLine3 NVARCHAR(100),
        City NVARCHAR(100),
        State NVARCHAR(100),
        CountryId BIGINT,
        Country NVARCHAR(50),
        Postcode NVARCHAR(20),
        Region NVARCHAR(50),
        ZoneCode NVARCHAR(20),
        CategoryCode NVARCHAR(20),
        Email NVARCHAR(MAX),
        SiteURL NVARCHAR(200),
        ContactPersonNumber NVARCHAR(20),
        ContactPersonName NVARCHAR(500),
        logo NVARCHAR(MAX),
        header NVARCHAR(MAX),
        footer NVARCHAR(MAX),
        CreatedBy BIGINT,
        CreatedDate NVARCHAR(20),
        IsActive BIT
    );

    -- Clean up the XML document handle
    EXEC sp_xml_removedocument @DocHandle;

	SET @CustomerId = SCOPE_IDENTITY()

    SELECT @CustomerId as CustomerId,@CustomerMnemonic as CustomerMnemonic FOR XML RAW('Json'),ELEMENTS
END