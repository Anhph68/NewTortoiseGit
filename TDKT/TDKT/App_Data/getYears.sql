USE [TDKT]
GO
/****** Object:  StoredProcedure [dbo].[getDonViDo]    Script Date: 7/1/2014 2:25:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[getDonViDo] @namkt NVARCHAR(5)
AS
    BEGIN
        SELECT  DISTINCT
                namkt = NAM_KT
              , socuoc = COUNT(*)
        FROM    dbo.CUOC_KT
        GROUP BY NAM_KT
        ORDER BY CUOC_KT.NAM_KT DESC
    END