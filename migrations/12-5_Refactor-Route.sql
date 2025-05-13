-- Delete userroute table
DROP TABLE IF EXISTS userroutes;

-- 1. Añadir la columna (tipo igual al de users.id, típicamente UUID o INTEGER)
ALTER TABLE route
ADD COLUMN id_user UUID;

-- 2. Crear la foreign key (asume que users.id es UUID)
ALTER TABLE route
ADD CONSTRAINT fk_route_user
FOREIGN KEY (id_user)
REFERENCES users(id_user)
ON DELETE CASCADE;

-- 3. Añadir nombres de calles
ALTER TABLE route
ADD COLUMN origin_street_name TEXT,
ADD COLUMN destination_street_name TEXT;