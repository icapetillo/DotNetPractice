-- ##########################################################################
-- # 1. Tablas Base (Sin dependencias FK)
-- ##########################################################################

-- Table: Publishers
CREATE TABLE Publishers (
    id INT IDENTITY(1,1) PRIMARY KEY, -- primary_key, auto_increment
    name VARCHAR(100) NOT NULL UNIQUE, -- unique
    country VARCHAR(100)
);
GO

-- Table: Formats
CREATE TABLE Formats (
    id INT IDENTITY(1,1) PRIMARY KEY, -- primary_key, auto_increment
    name VARCHAR(100) NOT NULL UNIQUE -- unique
);
GO

-- Table: Locations
CREATE TABLE Locations (
    id INT IDENTITY(1,1) PRIMARY KEY, -- primary_key, auto_increment
    name VARCHAR(100) NOT NULL UNIQUE -- unique
);
GO

-- ##########################################################################
-- # 2. Tablas Intermedias (Dependen de Publishers)
-- ##########################################################################

-- Table: Series
CREATE TABLE Series (
    id INT IDENTITY(1,1) PRIMARY KEY, -- primary_key, auto_increment
    name VARCHAR(100) NOT NULL, -- series name
    publisher_id INT NOT NULL, -- foreign key (ref publishers id)
    volume INT, -- series volume
    start_year INT, -- start year (optional)
    end_year INT, -- end year (optional)

    -- Restricción de Clave Foránea
    CONSTRAINT FK_Series_PublisherID FOREIGN KEY (publisher_id)
        REFERENCES Publishers(id)
);
GO

-- ##########################################################################
-- # 3. Tabla Principal (Depende de Series, Formats, Locations)
-- ##########################################################################

-- Table: Comics
CREATE TABLE Comics (
    id INT IDENTITY(1,1) PRIMARY KEY, -- primary_key, auto_increment
    series_id INT NOT NULL, -- fk (ref series id)
    number INT NOT NULL, -- issue number
    format_id INT NOT NULL, -- fk (ref format id)
    purchase_price DECIMAL(10, 2), -- price paid
    currency VARCHAR(3), -- currency identifier (MXN, USD)
    language VARCHAR(3), -- language id (ENG, SPA)
    location_id INT, -- fk (ref location id)
    notes TEXT, -- notes on the issue

    -- Restricción de Claves Foráneas
    CONSTRAINT FK_Comics_SeriesID FOREIGN KEY (series_id)
        REFERENCES Series(id),
    
    CONSTRAINT FK_Comics_FormatID FOREIGN KEY (format_id)
        REFERENCES Formats(id),
    
    CONSTRAINT FK_Comics_LocationID FOREIGN KEY (location_id)
        REFERENCES Locations(id)
);
GO