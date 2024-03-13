IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [AspNetRoles] (
    [Id] nvarchar(450) NOT NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AspNetUsers] (
    [Id] nvarchar(450) NOT NULL,
    [nombre] nvarchar(max) NOT NULL,
    [apellido] nvarchar(max) NOT NULL,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Autores] (
    [id] uniqueidentifier NOT NULL,
    [nombre] nvarchar(max) NOT NULL,
    [apellido] nvarchar(max) NOT NULL,
    [pais] nvarchar(max) NOT NULL,
    [descripcion] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Autores] PRIMARY KEY ([id])
);
GO

CREATE TABLE [Categorias] (
    [id] uniqueidentifier NOT NULL,
    [nombre] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Categorias] PRIMARY KEY ([id])
);
GO

CREATE TABLE [Clientes] (
    [id] uniqueidentifier NOT NULL,
    [nombre] nvarchar(max) NOT NULL,
    [apellido] nvarchar(max) NOT NULL,
    [correo] nvarchar(max) NOT NULL,
    [matricula] int NOT NULL,
    [foto_perfil] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Clientes] PRIMARY KEY ([id])
);
GO

CREATE TABLE [Editorial] (
    [id] uniqueidentifier NOT NULL,
    [nombre] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Editorial] PRIMARY KEY ([id])
);
GO

CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] nvarchar(450) NOT NULL,
    [ProviderKey] nvarchar(450) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserRoles] (
    [UserId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserTokens] (
    [UserId] nvarchar(450) NOT NULL,
    [LoginProvider] nvarchar(450) NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Reportes] (
    [id] uniqueidentifier NOT NULL,
    [fecha_prestamo] datetime2 NOT NULL,
    [cliente_id] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Reportes] PRIMARY KEY ([id]),
    CONSTRAINT [FK_Reportes_Clientes_cliente_id] FOREIGN KEY ([cliente_id]) REFERENCES [Clientes] ([id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Libros] (
    [id] uniqueidentifier NOT NULL,
    [nombre] nvarchar(max) NOT NULL,
    [paginas] int NOT NULL,
    [descripcion] nvarchar(max) NOT NULL,
    [edicion] int NOT NULL,
    [fecha_publicacion] datetime2 NOT NULL,
    [autor_id] uniqueidentifier NOT NULL,
    [categoria_id] uniqueidentifier NOT NULL,
    [editorial_id] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Libros] PRIMARY KEY ([id]),
    CONSTRAINT [FK_Libros_Autores_autor_id] FOREIGN KEY ([autor_id]) REFERENCES [Autores] ([id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Libros_Categorias_categoria_id] FOREIGN KEY ([categoria_id]) REFERENCES [Categorias] ([id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Libros_Editorial_editorial_id] FOREIGN KEY ([editorial_id]) REFERENCES [Editorial] ([id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Prestamos] (
    [id] uniqueidentifier NOT NULL,
    [fecha_prestamo] datetime2 NOT NULL,
    [fecha_entrega] datetime2 NOT NULL,
    [estado] nvarchar(max) NOT NULL,
    [cliente_id] uniqueidentifier NOT NULL,
    [libro_id] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Prestamos] PRIMARY KEY ([id]),
    CONSTRAINT [FK_Prestamos_Clientes_cliente_id] FOREIGN KEY ([cliente_id]) REFERENCES [Clientes] ([id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Prestamos_Libros_libro_id] FOREIGN KEY ([libro_id]) REFERENCES [Libros] ([id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
GO

CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
GO

CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
GO

CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
GO

CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
GO

CREATE INDEX [IX_Libros_autor_id] ON [Libros] ([autor_id]);
GO

CREATE INDEX [IX_Libros_categoria_id] ON [Libros] ([categoria_id]);
GO

CREATE INDEX [IX_Libros_editorial_id] ON [Libros] ([editorial_id]);
GO

CREATE INDEX [IX_Prestamos_cliente_id] ON [Prestamos] ([cliente_id]);
GO

CREATE INDEX [IX_Prestamos_libro_id] ON [Prestamos] ([libro_id]);
GO

CREATE INDEX [IX_Reportes_cliente_id] ON [Reportes] ([cliente_id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240313040442_Inicial', N'6.0.0');
GO

COMMIT;
GO

