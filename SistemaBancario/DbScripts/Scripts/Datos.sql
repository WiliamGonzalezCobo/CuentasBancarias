go
SET IDENTITY_INSERT [sysBanc].[tipo_transaccion] ON
INSERT [sysBanc].[tipo_transaccion] ([id_tipo_transaccion], [nombre]) VALUES (2, N'Consignacion')
INSERT [sysBanc].[tipo_transaccion] ([id_tipo_transaccion], [nombre]) VALUES (1, N'Retiro')
INSERT [sysBanc].[tipo_transaccion] ([id_tipo_transaccion], [nombre]) VALUES (3, N'Transferencia')
SET IDENTITY_INSERT [sysBanc].[tipo_transaccion] OFF
go
SET IDENTITY_INSERT [sysBanc].[tipo_identificacion] ON
INSERT [sysBanc].[tipo_identificacion] ([id_tipo_identificacion], [nombre]) VALUES (1, N'Cedula')
INSERT [sysBanc].[tipo_identificacion] ([id_tipo_identificacion], [nombre]) VALUES (2, N'Cedula Extrangeria')
SET IDENTITY_INSERT [sysBanc].[tipo_identificacion] OFF
go
SET IDENTITY_INSERT [sysBanc].[cuenta] ON
INSERT [sysBanc].[cuenta] ([id_Cuenta], [saldo], [fecha_creacion],[id_Cliente]) VALUES (1, 1000, CAST(0x9B3F0B00 AS Date),1)
INSERT [sysBanc].[cuenta] ([id_Cuenta], [saldo], [fecha_creacion],[id_Cliente]) VALUES (2, 2000, CAST(0x9C3F0B00 AS Date),1)
SET IDENTITY_INSERT [sysBanc].[cuenta] OFF
go
SET IDENTITY_INSERT [sysBanc].[cliente] ON
INSERT [sysBanc].[cliente] ([id_cliente], [nombres], [apellidos], [identificacion], [celular], [direccion], [id_tipo_identificacion]) VALUES (1, N'william fabian', N'gonzalez cobo', N'1026566851', N'1026566851', N'cr 1003 75 55  sur', 1)
SET IDENTITY_INSERT [sysBanc].[cliente] OFF
go
SET IDENTITY_INSERT [sysBanc].[usuario] ON
INSERT [sysBanc].[usuario] ([id_usuario], [usuario], [contrasenia], [id_cliente]) VALUES (1, N'wgonzalezc', N'123', 1)
SET IDENTITY_INSERT [sysBanc].[usuario] OFF
