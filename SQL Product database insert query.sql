- SQL Queries:
  - For Database Db_Mango_Product > Table: TblProducts
  - Once Add migration is added and update database command is executed
  - Run this SQL query for Adding Products:
  
INSERT INTO [dbo].[TblProducts]
([Name], [Price], [Description]
           ,[CategoryName]
           ,[ImageUrl])
VALUES
(
	'Samosa',
	15,
	'A samosa is a fried central Asian pastry with a savoury filling, including ingredients such as spiced potatoes, onions, peas. It is made into different shapes, including triangular, cone, or crescent, depending on the region.',
	'Appetizer',
	'https://upload.wikimedia.org/wikipedia/commons/thumb/c/cb/Samosachutney.jpg/800px-Samosachutney.jpg'
),
(
	'Paneer Tikka',
	13.99,
	'Paneer tikka or Paneer Soola or Chhena Soola is an Indian dish made from chunks of paneer/ chhena marinated in spices and grilled in a tandoor. It is a vegetarian alternative to chicken tikka and other meat dishes. It is a popular dish that is widely available in India and countries with an Indian diaspora',
	'Appetizer',
	'https://thewanderlustkitchen.com/wp-content/uploads/2022/11/Paneer-Tikka-Recipe-6.jpg'
),
(
	'Sweet Pie',
	10.99,
	'Fruity, nutty or chocolatey, decked out with pastry, meringue or crumble, these sumptuous desserts are sure to satisfy your sweet tooth.',
	'Dessert',
	'https://www.simplyrecipes.com/thmb/H6PpZRDqEk1a4WRHXg_RFVmNk3k=/750x0/filters:no_upscale():max_bytes(150000):strip_icc():format(webp)/__opt__aboutcom__coeus__resources__content_migration__simply_recipes__uploads__2020__07__Sweet-Cherry-Pie-LEAD-1-f6d36864e805440d8b6804ef47e3cd71.jpg'
),
(
	'Pav Bhaji',
	15,
	'Pav Bhaji is popular Indian street food where dinner rolls/buns are served with spicy mashed veggies topped with dollop of butter. Street food doesn’t get better than this!
This Pav Bhaji Recipe is spicy, so flavorful and can be easily made. If you have never had this in your life, you are seriously missing out!',
	'Entree',
	'https://www.cookwithmanali.com/wp-content/uploads/2018/05/Best-Pav-Bhaji-Recipe-800x1212.jpg'
),
(
	'Samosa Chaat',
	27,
	'This Samosa Chaat is a another popular Indian chaat where samosa is served with chole (chickpea curry).
Chaat is a broad term used for all Indian street food. It’s mostly spicy, tangy and topped with yogurt and chutneys.',
	'Snacks',
	'https://www.cookwithmanali.com/wp-content/uploads/2019/09/Samosa-Chaat-Recipe.jpg'
),
(
	'Kachodi',
	10,
	'Kachori is a deep-fried, spicy, stuffed pastry originating from the Marwar region of Rajasthan, India. Alternative names for the snack include kachauri, kochuri, kachodi and katchuri.',
	'Snacks',
	'https://www.funfoodfrolic.com/wp-content/uploads/2022/03/Kachori-1.jpg'
),
(
	'Lassi',
	35,
	'Lassi is an Indian yogurt–based beverage with a smoothie-like consistency. It has been called "the most popular and traditional yogurt-based drink" in India. It has also been described as the form in which yogurt "is most cherished and unbeatably popular',
	'Indian Cold Drinks',
	'https://i0.wp.com/thefoodhog.com/wp-content/uploads/2022/10/sweet-lassi-ALIA-MUKHERJEE-CC-by-SA-4.0-735x458.jpeg'
),
(
	'Bhel Puri',
	15,
	'Bhelpuri is a popular street food/ chaat from Mumbai made with puffed rice, sev, boiled potatoes, onion and chutneys. It is one flavor packed dish that is sweet, tangy, salty and spicy -- all at the same time.

The traditional version of bhelpuri recipe does not have too many veggies. But my version is packed with colorful veggies like corn, cucumber, tomatoes, peas etc. It has the same flavors and amazing textures. ',
	'Snacks',
	'https://www.cookshideout.com/wp-content/uploads/2016/09/Veg-Bhel_4S.jpg'
),
(
	'Pani Puri',
	20,
	'Pani stands for ‘water’ while puri stands for ‘puffed and crispy deep-fried ball’. These balls are made with wheat flour or semolina-kneaded dough. In common language, spicy and tangy water is filled inside the crispy puri, along with a spiced mashed potato mixture.',
	'Snacks',
	'https://www.indianveggiedelight.com/wp-content/uploads/2023/08/pani-puri.jpg'
),
