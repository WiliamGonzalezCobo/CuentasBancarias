use SistemaBancario
go
-- example
-- execute sysBanc.consultar_saldo_cuenta 1
create procedure sysBanc.consultar_saldo_cuenta(@id_cuenta int)
as
select c.saldo from sysBanc.cuenta c  
where c.id_Cuenta = @id_cuenta
go
-- example
-- execute sysBanc.consultar_cuentas_cliente 1
create procedure sysBanc.consultar_cuentas_cliente(@id_cliente int)
as
select c.id_Cuenta,c.saldo,c.fecha_creacion from sysBanc.cuenta c  
inner join sysBanc.cliente_cuenta cc on cc.id_cliente = @id_cliente
where cc.id_Cuenta = c.id_Cuenta
go
-- example
-- execute sysBanc.consultar_movimientos_cuenta 1
create procedure sysBanc.consultar_movimientos_cuenta(@id_cuenta int)
as
select t.id_transaccion,t.id_tipo_transaccion,tt.nombre,t.valor from sysBanc.cuenta c  
inner join sysBanc.transaccion t on t.id_Cuenta = c.id_Cuenta
inner join sysBanc.tipo_transaccion tt on tt.id_tipo_transaccion = t.id_tipo_transaccion
where c.id_Cuenta = @id_cuenta
go

-- example
--sysBanc.realizarTransaccion(@id_cuenta int,@monto int,@tipoTransacion int,@idCuentaDestino int)
-- execute sysBanc.realizarTransaccion 1,1000,2,null
-- execute sysBanc.realizarTransaccion 1,1000,3,2
create procedure sysBanc.realizarTransaccion(@id_cuenta int,@monto int,@tipoTransacion int,@idCuentaDestino int)
as
begin
BEGIN TRY    
BEGIN TRAN 
	if @tipoTransacion = 1 -- retiro
	begin
		if @monto < (select c.saldo from cuenta c where c.id_Cuenta = @id_cuenta) 
		begin
			update cuenta  set saldo = saldo - @monto where id_Cuenta = @id_cuenta
			insert transaccion (id_Cuenta,id_tipo_transaccion,valor,fecha) 
			values(@id_cuenta,@tipoTransacion,-@monto,GETDATE())
			end
		else
		begin
			RAISERROR ('Error: Saldo Insuficiente', -- Message text.  
           11, -- Severity,  
           1, -- State,  
           N'Error');
		end
	end	
	else if @tipoTransacion = 2 --consignacion
	begin
	if @monto > 0 
		begin
			update cuenta  set saldo = saldo + @monto where id_Cuenta = @id_cuenta
			insert transaccion (id_Cuenta,id_tipo_transaccion,valor,fecha) 
			values(@id_cuenta,@tipoTransacion,@monto,GETDATE())
			end
		else
		begin
			RAISERROR ('Error: monto invalido solo se pueden consignar valores positivos', -- Message text.  
			   11, -- Severity,  
			   1, -- State,  
			   N'Error');
		
			
		end
	end
	else if @tipoTransacion = 3 --trasferencia
	begin
	if @monto > 0 
		begin
			if @monto < (select c.saldo from cuenta c where c.id_Cuenta = @id_cuenta) 
			begin
				if exists (select 1 from cuenta where id_Cuenta = @idCuentaDestino)
				begin 	
					update cuenta  set saldo = saldo - @monto where id_Cuenta = @id_cuenta
					update cuenta  set saldo = saldo + @monto where id_Cuenta = @idCuentaDestino
					insert transaccion (id_Cuenta,id_tipo_transaccion,valor,fecha) 
					values(@id_cuenta,@tipoTransacion,-@monto,GETDATE())
					insert transaccion (id_Cuenta,id_tipo_transaccion,valor,fecha) 
					values(@idCuentaDestino,@tipoTransacion,@monto,GETDATE())
				end
				else
				begin
					RAISERROR ('Error: la cuenta destino no existe', -- Message text.  
				   11, -- Severity,  
				   1, -- State,  
				   N'Error');
				end
			end
			else
			begin
			RAISERROR ('Error: Saldo Insuficiente', -- Message text.  
			   11, -- Severity,  
			   1, -- State,  
			   N'Error');
			end
		end
		else
		begin
			RAISERROR ('Error: monto invalido solo se pueden transferir valores positivos', -- Message text.  
			   11, -- Severity,  
			   1, -- State,  
			   N'Error');
		end
	end
	else
	begin
	RAISERROR ('Error: operacion invalida', -- Message text.  
           11, -- Severity,  
           1, -- State,  
           N'Error');
	end
print 'commit'	
COMMIT TRAN        
END TRY
BEGIN CATCH
rollback tran
	DECLARE @vchErrorMessage VARCHAR(4000);  
	DECLARE @iErrorSeverity INT;  
	DECLARE @iErrorState INT;  
  
	SELECT   
		@vchErrorMessage = ERROR_MESSAGE(),  
		@iErrorSeverity = ERROR_SEVERITY(),  
		@iErrorState = ERROR_STATE();  

	RAISERROR (@vchErrorMessage, -- Message text.  
			   @iErrorSeverity, -- Severity.  
			   @iErrorState -- State.  
			   );  
END CATCH
end
go


