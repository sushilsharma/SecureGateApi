CREATE PROCEDURE [dbo].[SSP_MenuList]  --'<Json><ServicesAction>LoadMenuList</ServicesAction><UserName>12345678900</UserName><RoleMasterId>1</RoleMasterId><CultureId>1101</CultureId><TanentCode>001</TanentCode></Json>'
    @xmlDoc XML
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @intPointer INT;
    DECLARE @RoleMasterId BIGINT;
    DECLARE @CultureId BIGINT;
    DECLARE @MemberId BIGINT = 0;
    DECLARE @UserName NVARCHAR(500);
	DECLARE @TanentCode NVARCHAR(250);

    -- Prepare XML document
    EXEC sp_xml_preparedocument @intPointer OUTPUT, @xmlDoc;

    -- Extract UserName and CultureId from XML
    SELECT @UserName = tmp.[UserName],
           @CultureId = tmp.[CultureId],
		   @RoleMasterId = tmp.[RoleMasterId],
		   @TanentCode =tmp.[TanentCode]
    FROM OPENXML(@intPointer, 'Json', 2)
         WITH ([UserName] NVARCHAR(500), [CultureId] BIGINT , [RoleMasterId] bigint ,[TanentCode]  NVARCHAR(250)) tmp;

    -- Clean up XML document
    EXEC sp_xml_removedocument @intPointer;

    -- Fetch UserId in a single statement
    SELECT TOP 1 
        @MemberId = MemberId,
		@TanentCode=TanentCode
    FROM Members
    WHERE ContactNumber = @UserName or Email=@UserName;

    -- Check if user has role-page mapping for specific conditions
    IF NOT EXISTS (
        SELECT 1
        FROM RoleWisePageMapping crwpm
        JOIN Pages p ON crwpm.PageId = p.PageId
        WHERE  crwpm.LoginId = @MemberId
          AND crwpm.RoleMasterId = @RoleMasterId
          AND p.IsActive = 1
          AND crwpm.IsActive = 1
		  AND crwpm.TanentCode=@TanentCode
    )
    BEGIN
        SET @MemberId = 0;
    END;

    WITH XMLNAMESPACES ('http://james.newtonking.com/projects/json' AS json)
    SELECT CAST((
        SELECT 
            'true' AS [@json:Array],
            p.[PageId],
            p.[PageName],
            p.ResourceKey AS ResourcePageName,
            p.[ParentPageId],
            p.[ControllerName],
            p.[ActionName],
            p.[IsReport],
            p.[PageIcon],
            p.[SequenceNumber],
            ISNULL(p.[ModuleId], 0) AS [ModuleId],
            p.[Description],
            p.[IsPhoneNo],
            p.[IsReloadForcefully],
            p.[UserPersonaMasterId],
            p.[IsHomePageForUserPersona],
            p.[IsVisibleInMenu],
            ISNULL(p.IsCommingSoonIndicator, '0') AS [IsCommingSoonIndicator]
             ,(Cast((SELECT 'true' AS [@json:Array] ,
                cp.[PageId],
                cp.[PageName],
                cp.ResourceKey AS ResourcePageName,
                cp.[ParentPageId],
                cp.[ControllerName],
                cp.[ActionName],
                cp.[IsReport],
                cp.[PageIcon],
                ISNULL(cp.[ModuleId], 0) AS [ModuleId],
                cp.[SequenceNumber],
                cp.[Description],
                cp.[IsPhoneNo],
                cp.[IsReloadForcefully],
                cp.[UserPersonaMasterId],
                cp.[IsHomePageForUserPersona],
                cp.[IsVisibleInMenu],
                cp.[IsActive],
                ISNULL(cp.IsCommingSoonIndicator, '0') AS [IsCommingSoonIndicator]
            FROM Pages cp
            JOIN RoleWisePageMapping crwpm ON cp.PageId = crwpm.PageId AND crwpm.LoginId = @MemberId
            WHERE cp.IsActive = 1 
              AND cp.ParentPageId = p.PageId 
              AND crwpm.RoleMasterId = @RoleMasterId  
              AND crwpm.IsActive = 1
			  AND crwpm.TanentCode=@TanentCode
            ORDER BY cp.SequenceNumber ASC
           FOR XML path('PagesList'),ELEMENTS) AS XML))


    FROM Pages p
    JOIN RoleWisePageMapping rwpm ON p.PageId = rwpm.PageId AND rwpm.LoginId = @MemberId
    WHERE p.IsActive = 1 
      AND (p.ParentPageId = 0 OR p.ParentPageId IS NULL)
      AND ISNULL(p.IsInnerPage, 0) = 0
      AND rwpm.RoleMasterId = @RoleMasterId  
      AND rwpm.IsActive = 1
	  AND rwpm.TanentCode=@TanentCode
    ORDER BY p.SequenceNumber ASC
    FOR XML PATH('PagesList'), ELEMENTS, ROOT('Json')) AS XML)
END;