Create database Challenge;

-- Create the Artists table
CREATE TABLE Artists (
ArtistID INT PRIMARY KEY,
Name VARCHAR(255) NOT NULL,
Biography TEXT,
Nationality VARCHAR(100));

-- Create the Categories table
CREATE TABLE Categories (
CategoryID INT PRIMARY KEY,
Name VARCHAR(100) NOT NULL);

-- Create the Artworks table
CREATE TABLE Artworks (
ArtworkID INT PRIMARY KEY,
Title VARCHAR(255) NOT NULL,
ArtistID INT,
CategoryID INT,
Year INT,
Description TEXT,
ImageURL VARCHAR(255),
FOREIGN KEY (ArtistID) REFERENCES Artists (ArtistID),
FOREIGN KEY (CategoryID) REFERENCES Categories (CategoryID));

-- Create the Exhibitions table
CREATE TABLE Exhibitions (
ExhibitionID INT PRIMARY KEY,
Title VARCHAR(255) NOT NULL,
StartDate DATE,
EndDate DATE,
Description TEXT);

-- Create a table to associate artworks with exhibitions
CREATE TABLE ExhibitionArtworks (
ExhibitionID INT,
ArtworkID INT,
PRIMARY KEY (ExhibitionID, ArtworkID),
FOREIGN KEY (ExhibitionID) REFERENCES Exhibitions (ExhibitionID),
FOREIGN KEY (ArtworkID) REFERENCES Artworks (ArtworkID));

-- Insert sample data into the Artists table
INSERT INTO Artists (ArtistID, Name, Biography, Nationality) VALUES
(1, 'Pablo Picasso', 'Renowned Spanish painter and sculptor.', 'Spanish'),
(2, 'Vincent van Gogh', 'Dutch post-impressionist painter.', 'Dutch'),
(3, 'Leonardo da Vinci', 'Italian polymath of the Renaissance.', 'Italian'),
(4, 'Claude Monet', 'French impressionist painter.', 'French'),
(5, 'Frida Kahlo', 'Mexican painter known for self-portraits.', 'Mexican'),
(6, 'Michelangelo', 'Italian sculptor and painter.', 'Italian'),
(7, 'Salvador Dalí', 'Spanish surrealist artist.', 'Spanish'),
(8, 'Georgia O''Keeffe', 'American modernist artist.', 'American'),
(9, 'Rembrandt', 'Dutch painter and etcher.', 'Dutch'),
(10, 'Andy Warhol', 'American pop art pioneer.', 'American');

-- Insert sample data into the Categories table
INSERT INTO Categories (CategoryID, Name) VALUES
(1, 'Painting'),
(2, 'Sculpture'),
(3, 'Photography');

-- Insert sample data into the Artworks table
INSERT INTO Artworks (ArtworkID, Title, ArtistID, CategoryID, Year, Description, ImageURL) VALUES
(1, 'Starry Night', 2, 1, 1889, 'A famous painting by Vincent van Gogh.', 'starry_night.jpg'),
(2, 'Mona Lisa', 3, 1, 1503, 'The iconic portrait by Leonardo da Vinci.', 'mona_lisa.jpg'),
(3, 'Guernica', 1, 1, 1937, 'Pablo Picassos powerful anti-war mural.', 'guernica.jpg'),
(4, 'The Persistence of Memory', 7, 1, 1931, 'Dalís surreal melting clocks.', 'persistence_memory.jpg'),
(5, 'The Scream', 9, 1, 1893, 'Munchs iconic expressionist work.', 'scream.jpg'),
(6, 'David', 6, 2, 1504, 'Michelangelos marble masterpiece.', 'david.jpg'),
(7, 'Campbells Soup Cans', 10, 1, 1962, 'Warhols pop art series.', 'soup_cans.jpg'),
(8, 'The Two Fridas', 5, 1, 1939, 'Kahlos double self-portrait.', 'two_fridas.jpg'),
(9, 'Water Lilies', 4, 1, 1916, 'Monets impressionist series.', 'water_lilies.jpg'),
(10, 'The Night Watch', 9, 1, 1642, 'Rembrandts large group portrait.', 'night_watch.jpg');


-- Insert sample data into the Exhibitions table
INSERT INTO Exhibitions (ExhibitionID, Title, StartDate, EndDate, Description) VALUES
(1, 'Modern Art Masterpieces', '2023-01-01', '2023-03-01', 'Collection of modern art masterpieces.'),
(2, 'Renaissance Art', '2023-04-01', '2023-06-01', 'Showcase of Renaissance art treasures.'),
(3, 'Surrealism Explored', '2023-07-01', '2023-09-01', 'Journey through surrealist works.'),
(4, 'Impressionist Wonders', '2023-10-01', '2023-12-01', 'Celebration of impressionist art.'),
(5, 'Contemporary Voices', '2024-01-01', '2024-03-01', 'Modern artists and their visions.');

-- Insert artworks into exhibitions
INSERT INTO ExhibitionArtworks (ExhibitionID, ArtworkID) VALUES
(1, 1), (1, 3), (1, 4), (1, 7),
(2, 2), (2, 6),
(3, 4),
(4, 1), (4, 9),
(5, 7), (5, 8),
(1, 8), (3, 7), (4, 5), (5, 10);

--1. Retrieve the names of all artists along with the number of artworks they have in the gallery, and
--   list them in descending order of the number of artworks.
Select a.Name, count(aw.ArtworkID) as ArtworkCount from Artists a
left join Artworks aw on a.ArtistID=aw.ArtistID 
group by a.Name order by ArtworkCount desc;

--2. List the titles of artworks created by artists from 'Spanish' and 'Dutch' nationalities, and order
--   them by the year in ascending order.
Select aw.Title, a.Nationality from Artworks aw
join Artists a on aw.ArtistID = a.ArtistID where a.Nationality = 'Spanish' or a.Nationality= 'Dutch'
order by aw.Year;

--3. Find the names of all artists who have artworks in the 'Painting' category, and the number of
--   artworks they have in this category.
Select a.Name, count(aw.ArtworkID) as PaintingCount from Artists a
join Artworks aw on a.ArtistID=aw.ArtistID
join Categories c on aw.CategoryID=c.CategoryID where c.Name = 'Painting' group by a.Name;

--4. List the names of artworks from the 'Modern Art Masterpieces' exhibition, along with their
--   artists and categories.
Select aw.Title, a.Name as Artist, c.Name as Category from Artworks aw
join Artists a on aw.ArtistID=a.ArtistID 
join Categories c on aw.CategoryID=c.CategoryID
join ExhibitionArtworks ea on aw.ArtistID=ea.ArtworkID
join Exhibitions e on ea.ExhibitionID=e.ExhibitionID where e.Title= 'Modern Art Masterpieces';

--5. Find the artists who have more than two artworks in the gallery.
Select a.Name, count(aw.ArtworkID) as ArtworkCount from Artists a
join Artworks aw on a.ArtistID=aw.ArtistID 
group by a.Name having count(aw.ArtworkID) > 2;

INSERT INTO Artworks (ArtworkID, Title, ArtistID, CategoryID, Year, Description, ImageURL) VALUES
(11, 'Les Demoiselles d.Avignon', 1, 1, 1907, 'Picassos proto-cubist work', 'demoiselles.jpg'),
(12, 'The Old Guitarist', 1, 1, 1903, 'Picassos Blue Period masterpiece', 'guitarist.jpg');

--6. Find the titles of artworks that were exhibited in both 'Modern Art Masterpieces' and
--   'Renaissance Art' exhibitions
select aw.Title from Artworks aw
join ExhibitionArtworks ea1 on aw.ArtworkID=ea1.ArtworkID
join Exhibitions e1 on ea1.ExhibitionID=e1.ExhibitionID
join ExhibitionArtworks ea2 on aw.ArtworkID=ea2.ArtworkID
join Exhibitions e2 on ea2.ExhibitionID=e2.ExhibitionID
where e1.Title='Modern Art Masterpieces' and e2.Title='Renaissance Art';

INSERT INTO ExhibitionArtworks (ExhibitionID, ArtworkID) VALUES
(1,6),(2,3);

--7. Find the total number of artworks in each category
Select c.Name, count(aw.ArtworkID) As NumberOfArtworks from Categories c
left join Artworks aw on c.CategoryID=aw.CategoryID group by c.Name order by NumberOfArtworks;

--8. List artists who have more than 3 artworks in the gallery.
Select a.Name, count(aw.ArtworkID) as ArtworkCount from Artists a
join Artworks aw on a.ArtistID=aw.ArtistID
join ExhibitionArtworks ea on aw.ArtworkID=ea.ArtworkID
group by a.Name having count(aw.ArtworkID)>3;

INSERT INTO Artworks (ArtworkID, Title, ArtistID, CategoryID, Year, Description, ImageURL) VALUES
(13, 'The Weeping Woman', 1, 1, 1937, 'Picassos cubist portrait of Dora Maar', 'weeping_woman.jpg'),
(14, 'Girl Before a Mirror', 1, 1, 1932, 'Picassos colorful abstract portrait', 'girl_mirror.jpg'),
(15, 'The Last Supper', 3, 1, 1498, 'Da Vincis famous mural in Milan', 'last_supper.jpg'),
(16, 'Vitruvian Man', 3, 1, 1490, 'Iconic study of human proportions', 'vitruvian_man.jpg'),
(17, 'Lady with an Ermine', 3, 1, 1489, 'Portrait of Cecilia Gallerani', 'ermine_lady.jpg'),
(18, 'Salvator Mundi', 3, 1, 1500, 'Christ as Savior of the World', 'salvator_mundi.jpg');

INSERT INTO ExhibitionArtworks (ExhibitionID, ArtworkID) VALUES
(1,13),(2,14),(3,15),(4,16),(5,17),(3,18);

--9. Find the artworks created by artists from a specific nationality (e.g., Spanish).
Select aw.Title from Artworks aw
join Artists a on aw.ArtistID=a.ArtistID where a.Nationality='Spanish';

--10. List exhibitions that feature artwork by both Vincent van Gogh and Leonardo da Vinci.
Select e.Title from Exhibitions e where e.ExhibitionID in (
Select ea.ExhibitionID from ExhibitionArtworks ea 
join Artworks aw on ea.ArtworkID=aw.ArtworkID
join Artists a on aw.ArtistID=a.ArtistID
where a.Name = 'Vincent van Gogh')
and e.ExhibitionID in ( 
Select ea.ExhibitionID from ExhibitionArtworks ea 
join Artworks aw on ea.ArtworkID=aw.ArtworkID
join Artists a on aw.ArtistID=a.ArtistID
where a.Name = 'Leonardo da Vinci');

--11. Find all the artworks that have not been included in any exhibition.
Select aw.Title from Artworks aw 
left join ExhibitionArtworks ea on aw.ArtworkID=ea.ArtworkID
where ea.ExhibitionID is null;

--12. List artists who have created artworks in all available categories.
Select a.Name from Artists a
join Artworks aw on a.ArtistID=aw.ArtistID
join Categories c on aw.CategoryID=c.CategoryID
group by a.Name having COUNT(DISTINCT c.CategoryID) = 3;

INSERT INTO Artists (ArtistID, Name, Biography, Nationality) VALUES
(11, 'Thomas', 'Contemporary artist working in all mediums', 'International');

INSERT INTO Artworks (ArtworkID, Title, ArtistID, CategoryID, Year, Description, ImageURL) VALUES
(19, 'Color Explosion', 11, 1, 2020, 'Abstract painting', 'color_explosion.jpg'),     
(20, 'Marble Vision', 11, 2, 2021, 'Modern sculpture', 'marble_vision.jpg'),             
(21, 'Urban Shadows', 11, 3, 2022, 'Black and white photography', 'urban_shadows.jpg');

--13. List the total number of artworks in each category.
Select c.Name, count(aw.ArtworkID) as ArtworkCount from Artworks aw
join Categories c on aw.CategoryID=c.CategoryID group by c.Name;

--14. Find the artists who have more than 2 artworks in the gallery.
Select a.Name from Artists a
join Artworks aw on a.ArtistID=aw.ArtistID
group by a.Name having count(aw.ArtworkID)>2; 

--15. List the categories with the average year of artworks they contain, only for categories with more
--    than 1 artwork.
Select c.Name as CategoryName, avg(aw.Year) AS AvgYear from Categories c
join Artworks aw on c.CategoryID=aw.CategoryID 
group by c.Name having count(aw.ArtistID)>1;

--16. Find the artworks that were exhibited in the 'Modern Art Masterpieces' exhibition.
Select aw.Title from Artworks aw
join ExhibitionArtworks ea on aw.ArtworkID=ea.ArtworkID
join Exhibitions e on ea.ExhibitionID=e.ExhibitionID
where e.Title='Modern Art Masterpieces';

--17. Find the categories where the average year of artworks is greater than the average year of all
--    artworks.
Select c.Name as CategoryName, avg(aw.Year) AS AvgYear from Categories c
join Artworks aw on c.CategoryID=aw.CategoryID 
group by c.Name having avg(aw.Year)>( select avg(aw.Year) from Artworks aw);

--18. List the artworks that were not exhibited in any exhibition.
Select aw.Title from Artworks aw
left join ExhibitionArtworks ea on aw.ArtworkID=ea.ArtworkID
 where ea.ExhibitionID is null;

 --19. Show artists who have artworks in the same category as "Mona Lisa."
Select a.Name, aw.CategoryID from Artists a
join Artworks aw on a.ArtistID = aw.ArtistID
where aw.CategoryID = ( select aw.CategoryID from Artworks aw
where aw.Title = 'Mona Lisa');

 --20. List the names of artists and the number of artworks they have in the gallery.
 Select a.Name, count(aw.ArtworkID) as ArtworkCount from Artists a
 join Artworks aw on a.ArtistID=aw.ArtistID group by a.Name;
