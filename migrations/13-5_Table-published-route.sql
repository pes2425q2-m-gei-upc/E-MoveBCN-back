-- Create published route table
CREATE TABLE published_route (
  route_id UUID PRIMARY KEY,
  publish_date DATE NOT NULL,
  available_seats INTEGER NOT NULL CHECK (available_seats >= 0),
  CONSTRAINT fk_published_route_route
    FOREIGN KEY (route_id)
    REFERENCES route(route_id)
    ON DELETE CASCADE
);