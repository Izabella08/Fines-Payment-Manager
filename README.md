# Fines-Payment-Manager
The application consists of three components which can be accessed using a username and a password. 
One component is used by the post office employees to register the persons that have paid a traffic fine. 
The other component is used by the police employees to do the following operations: 
  (1) add/update/delete the drivers’ information (name, address, driving license details, identity card details), 
  (2) add/update/delete fines, 
  (3) create reports.  --using Factory DP
When a post office employee registers a person that has paid a traffic fine, the application notifies the police employees from the corresponding police station. All the information about users, drivers and fines is stored in a database. --using Observer DP
The last component is used by the driver to add a complaint. A police employee can also update/delete complaints.
