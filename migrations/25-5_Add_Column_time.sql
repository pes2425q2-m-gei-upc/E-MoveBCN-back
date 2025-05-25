-- Add column `hour` to `published_route` table
ALTER TABLE published_route
ADD COLUMN hour TIME DEFAULT CURRENT_TIME;