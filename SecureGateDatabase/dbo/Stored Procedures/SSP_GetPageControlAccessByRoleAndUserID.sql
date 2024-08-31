CREATE PROCEDURE [dbo].[SSP_GetPageControlAccessByRoleAndUserID] --'<Json><ServicesAction>LoadMenuList</ServicesAction><UserName>12345678900</UserName><RoleMasterId>1</RoleMasterId><CultureId>1101</CultureId><TanentCode>001</TanentCode></Json>'
    @xmlDoc XML
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @intPointer INT;
    DECLARE @RoleMasterId BIGINT;
    DECLARE @MemberId BIGINT;
    DECLARE @UserName NVARCHAR(250);
    DECLARE @TanentCode NVARCHAR(250);
    DECLARE @PageUrl NVARCHAR(250);
    DECLARE @PageType BIGINT;

    -- Prepare XML document
    EXEC sp_xml_preparedocument @intPointer OUTPUT, @xmlDoc;

    -- Extract parameters from XML using OPENXML
    SELECT 
        @UserName = tmp.UserName,
        @PageType = tmp.PageType,
        @PageUrl = tmp.pageUrl,
        @RoleMasterId = tmp.RoleMasterId,
        @TanentCode = tmp.TanentCode
    FROM OPENXML(@intPointer, '/Json', 2)
    WITH (
        UserName NVARCHAR(250),
        PageType BIGINT,
        pageUrl NVARCHAR(250),
        RoleMasterId BIGINT,
        TanentCode NVARCHAR(250)
    ) tmp;

    -- Clean up XML document
    EXEC sp_xml_removedocument @intPointer;

    -- Fetch UserId and TenantCode
    SELECT TOP 1 
        @MemberId = MemberId,
        @TanentCode = TanentCode
    FROM Members
    WHERE ContactNumber = @UserName OR Email = @UserName;




    -- Determine RoleMasterId or MemberId based on existence in RoleWiseFieldAccess
    IF EXISTS (SELECT 1 FROM RoleWiseFieldAccess WHERE LoginId = @MemberId)
    BEGIN
        SET @RoleMasterId = 0;
    END
    ELSE
    BEGIN
        SET @MemberId = 0;
    END;

    -- Query to generate XML result efficiently
    ;WITH XMLNAMESPACES('http://james.newtonking.com/projects/json' AS json)
    SELECT 
        CAST((SELECT 
            'true' AS [@json:Array],
            pc.ControlName, 
            pc.ResourceKey, 
            ISNULL(rwfa.AccessId, 0) AS AccessId,
            ISNULL(pc.DataSource, '0') AS DataSource,
            rwfa.RoleId,
            rwfa.LoginId,
            p.PageName,
            p.ControllerName AS PageURL,
            ISNULL(pc.IsMandatory, 0) AS IsMandatory,
            ISNULL(pc.ValidationExpression, '-') AS ValidationExpression,
            pc.ControlType
        FROM PageControl pc
         JOIN RoleWiseFieldAccess rwfa 
            ON pc.PageId = rwfa.PageId 
            AND pc.PageControlId = rwfa.PageControlId 
            AND rwfa.IsActive = 1
         JOIN Pages p 
            ON p.PageId = pc.PageId
        WHERE pc.IsActive = 1  and rwfa.IsActive = 1 and p.IsActive = 1 
          AND (rwfa.RoleId = @RoleMasterId AND rwfa.LoginId = @MemberId)
          AND rwfa.TanentCode = @TanentCode
        FOR XML PATH('PageControlList'), ELEMENTS, ROOT('Json')) AS XML) AS Result;
END;