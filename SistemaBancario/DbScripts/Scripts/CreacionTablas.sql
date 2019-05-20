create database SistemaBancario
go
use SistemaBancario
go
CREATE SCHEMA sysBanc;
go
create table sysBanc.tipo_transaccion(
id_tipo_transaccion int primary Key identity,
nombre varchar(50) not null unique
)
go
create table sysBanc.tipo_identificacion(
id_tipo_identificacion  int primary Key identity,
nombre varchar(50) not null unique
)
go
create table sysBanc.cliente(
id_cliente int primary Key identity,
nombres varchar(50) not null,
apellidos varchar(50) not null,
identificacion varchar(50) not null unique,
celular varchar(50)not null,
direccion varchar(50) not null,
id_tipo_identificacion int not null,
constraint fk_tipo_identificacion 
foreign key (id_tipo_identificacion) 
references sysBanc.tipo_identificacion(id_tipo_identificacion)
)  
go
create table sysBanc.cuenta(
id_Cuenta int primary Key identity,
saldo int not null,
fecha_creacion date not null,
id_Cliente int not null,
constraint fk_cliente_cta foreign key(id_Cliente)
references sysBanc.cliente(id_cliente)

)
go
create table sysBanc.transaccion(
id_transaccion int primary Key identity,
valor int not null,
fecha datetime not null,
id_tipo_transaccion int not null,
id_Cuenta int not null,
constraint fk_tipotransaccion 
foreign key (id_tipo_transaccion) 
references sysBanc.tipo_transaccion(id_tipo_transaccion),
constraint fk_cuenta 
foreign key (id_Cuenta) 
references sysBanc.cuenta(id_Cuenta)
)  
go
create table sysBanc.usuario(
id_usuario int primary Key identity,
usuario varchar(50) not null unique,
contrasenia varchar(50) not null,
id_cliente int not null unique,
constraint fk_cliente 
foreign key (id_cliente)
references sysBanc.cliente(id_cliente)
)
go
--create table sysBanc.cliente_cuenta(
--id_cliente_cuenta int primary key identity,
--id_cliente int not null,
--id_Cuenta int not null unique,
--)