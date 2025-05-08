-- Paso 1: Borrar datos de saved_ubications
DELETE FROM saved_ubications;

-- Paso 2: Alterar tabla user
ALTER TABLE "users"
RENAME COLUMN name TO username;

-- Quitar UNIQUE de username (si existe)
ALTER TABLE "users"
DROP CONSTRAINT IF EXISTS unique_user_name;

-- Asegurar que email sea UNIQUE
ALTER TABLE "users"
ADD CONSTRAINT user_email_key UNIQUE(email);

-- Paso 3: Cambiar relación en saved_ubications
-- Asumiendo que hay una FK existente, primero eliminarla
ALTER TABLE saved_ubications
DROP CONSTRAINT saved_ubications_username_fkey;

-- Eliminar columna username
ALTER TABLE saved_ubications
DROP COLUMN IF EXISTS username;

-- Añadir columna email para nueva FK
ALTER TABLE "saved_ubications"
ADD COLUMN email TEXT;

-- Añadir FK por email
ALTER TABLE saved_ubications
ADD CONSTRAINT saved_ubications_user_email_fkey FOREIGN KEY (email) REFERENCES "users"(email);
