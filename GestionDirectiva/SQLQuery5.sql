USE [GP_PMANAGEMENT]
GO
/****** Object:  StoredProcedure [dbo].[GENERAR_DIAS]    Script Date: 05/23/2016 11:21:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-------------------------------------------------------------- PROCEDURE ---------------------------------------------------------------------------  
ALTER PROCEDURE [dbo].[GENERAR_DIAS]  
AS  
  
DECLARE  
@ID_PROMOCION          int = 1, -- OK
@ID_SEMANA             int = 1, -- OK
@CantidadRegistros     int=0,   -- OK
@Semanas_Promocion     int=0,   -- OK
@CantidadPromociones   int=0,    -- OK
@CantidadDias          int=1,
---------------------------------------
@Contador_Promociones  int=1,
@Contador_Semana       int=1,
@Contador_Dias         int=0, 
--------------------------------------
@Fec_Inicio           Datetime

Create Table #TEMP_DIAS_PROMOCION  
(  
 [FECHA]         [Datetime],  
 [ID_PROMOCION]  [Int],  
 [ID_SEMANA]     [Int]  
)  
  
set @CantidadRegistros = (select COUNT(*) from TB_DETALLE_PROMOCION) -- 25  
set @CantidadPromociones = (select COUNT(id_Promocion) from TB_PROMOCION) --2
  
while (@Contador_Promociones <=@CantidadPromociones) --> En  
BEGIN
print 'Id_Contador_Promociones:'+ convert(varchar,@Contador_Promociones)
------------------------------------------------------------------BEGIN PRIMER WHILE ------------------------------------------------------------   
set @Semanas_Promocion = (select COUNT(*) from TB_DETALLE_PROMOCION where Id_Promocion = @Contador_Promociones)  
while (@Contador_Semana <= @Semanas_Promocion)  
BEGIN
----------------------------------------------------------------BEGIN SEGUNDO WHILE ------------------------------------------------------------   
set @CantidadDias = (select Dias       from TB_DETALLE_PROMOCION where Id_Promocion = @Contador_Promociones and Id_Semana = @Contador_Semana) 
set @Fec_Inicio =   (select Fec_Inicio from TB_DETALLE_PROMOCION where Id_Promocion = @Contador_Promociones and Id_Semana = @Contador_Semana) 
      while (@Contador_Dias <= @CantidadDias)  
  BEGIN
  if(@Contador_Dias = 0)
  Begin
    print 'Dias_Por_Promocion:'+ convert(varchar,@Fec_Inicio)
    insert into TB_DIAS_PROMOCION(Fecha,Id_Promocion,Id_Semana)
    values (@Fec_Inicio,@Contador_Promociones,@Contador_Semana)
    set @Contador_Dias = @Contador_Dias+1
  End
  else
  Begin
    set @Fec_Inicio = DATEADD(day,1,@Fec_Inicio) 
    print 'Dias_Por_Promocion:'+ convert(varchar,@Fec_Inicio)
    insert into TB_DIAS_PROMOCION(Fecha,Id_Promocion,Id_Semana)
    values (@Fec_Inicio,@Contador_Promociones,@Contador_Semana)
    set @Contador_Dias = @Contador_Dias+1
  End
  END 

set @Contador_Dias = 0
----------------------------------------------------------------END   SEGUNDO WHILE -------------------------------------------------------------- 
print 'Semanas_Por_Promocion_Semana:'+ convert(varchar,@Contador_Semana)
set @Contador_Semana = @Contador_Semana+1
END
set @Contador_Semana = 1
----------------------------------------------------------------END   PRIMER WHILE -------------------------------------------------------------- 
set @Contador_Promociones = @Contador_Promociones+1
END