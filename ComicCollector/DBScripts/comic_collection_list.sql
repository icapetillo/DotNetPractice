--INSERT INTO dbo.Comics (series_id, number, format_id, purchase_price, currency, language, location_id, notes) 
--VALUES (1,1,6,79,'MXN','SPA',24,NULL);

SELECT 
    series.name as "TITULO",
    comics.number as "NUMERO",
    formats.name as "FORMATO",
    comics.purchase_price as "PRECIO PORTADA",
    comics.currency as "MONEDA",
    publishers.name as "EDITORIAL",
    comics.language as "IDIOMA",
    locations.name as "UBICACIÓN"
FROM dbo.Comics AS comics
JOIN dbo.Series AS series
    ON comics.series_id = series.id
JOIN dbo.Formats AS formats
    ON comics.format_id = formats.id
JOIN dbo.Publishers as publishers
    ON series.publisher_id = publishers.id
JOIN dbo.Locations AS locations
    ON comics.location_id = locations.id;