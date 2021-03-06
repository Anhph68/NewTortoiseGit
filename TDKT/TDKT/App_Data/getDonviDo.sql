USE [TDKT]
GO
/****** Object:  StoredProcedure [dbo].[getDonViDo]    Script Date: 7/1/2014 1:53:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[getDonViDo] @namkt NVARCHAR(5)
AS
    BEGIN
        SELECT  DISTINCT
                MA
              , TEN
        FROM    dbo.TD_DVKT
                INNER JOIN dbo.CUOC_KT
                    ON CUOC_KT.MA_DVKT = TD_DVKT.MA
        WHERE   NAM_KT = @namkt
        ORDER BY MA ASC
    END