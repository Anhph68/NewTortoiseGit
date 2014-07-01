CREATE PROC getYear
AS
    BEGIN
        SELECT  DISTINCT
                namkt = NAM_KT
              , socuoc = ( SELECT   COUNT(*)
                           FROM     dbo.CUOC_KT
                           GROUP BY NAM_KT )
        FROM    dbo.CUOC_KT
    END