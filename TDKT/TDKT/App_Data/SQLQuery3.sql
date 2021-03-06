USE [TDKT]
GO
/****** Object:  StoredProcedure [dbo].[genCode]    Script Date: 6/21/2014 9:12:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[genCode]
    @namkt NVARCHAR(5)
  , @donvi NVARCHAR(30)
  , @linhvuc NVARCHAR(30)
  , @loaihinh NVARCHAR(30)
AS
    BEGIN
        SELECT TOP ( 1 )
                'C' + RIGHT(@namkt, 2) + '-' + @donvi + '-'
                + @linhvuc + '-' + +@loaihinh + '-'
                + RIGHT(10001
                        + RIGHT(ISNULL(( SELECT TOP ( 1 )
                                            MA_CUOC
                                         FROM
                                            dbo.CUOC_KT
                                         WHERE
                                            NAM_KT = @namkt
                                            AND MA_DVKT = @donvi
                                            AND MA_LHKT = @loaihinh
                                            AND MA_LVKT = @linhvuc
                                         ORDER BY MA_CUOC DESC ),
                                       '0000'), 4), 4)
        FROM    dbo.CUOC_KT
    END