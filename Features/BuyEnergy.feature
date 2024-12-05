Feature: BuyEnergy

This feature covers scenarios regarding buying units of fuel using api endpoints and validating the messages received.


Scenario: Verify User Can Reset Test data using the POST /ENSEK/reset endpoint
	Given I login to obtain access token
	And I reset the test data back to its initial state
	Then The user should see a success message

Scenario: Verify User can get the count of orders using the GET /ENSEK/orders endpoint
	Given I login to obtain access token
	When The user sends GET request to obtain details of previous orders
	Then The user should see a total of 5 orders

Scenario: Verify User Can Buy Energy Based On Quantity Of Units Available using PUT /ENSEK​/buy​/{id}​/{quantity} endpoint
	Given I login to obtain access token
	When The user buys <quantity> quantity of <fuelType> fuel
	Then The user would get back response message <Message>
	Examples: 
	| quantity | fuelType | Message                                                                                  |
	| 10       | gas      | You have purchased 10 m³ at a cost of 3.4000000000000004 there are 2990 units remaining. |
	| 10       | electric | You have purchased 10 kWh at a cost of 4.699999999999999 there are 4312 units remaining. |
	| 9        | nuclear  | There is no nuclear fuel to purchase!                                                    |
	| 5        | oil      | You have purchased 5 Litres at a cost of 2.5 there are 15 units remaining.               |

Scenario: Verify that the previous order is present in the orders list with the expected details using GET /ENSEK​/orders/{orderId} endpoint
	Given I login to obtain access token
	When The user buys <quantity> quantity of <fuelType> fuel
	And The user sends request to obtain details of previous order with order id 31fc32da-bccb-44ab-9352-4f43fc44ed4b
	Then The user should see <id> details of previous orders
	Examples: 
	| quantity | fuelType | id                                   |
	| 20       | electric | 31fc32da-bccb-44ab-9352-4f43fc44ed4b |

Scenario: Verify orders that were created before the current date are returned using GET /ENSEK​/orders/{orderId} endpoint
	Given I login to obtain access token
	When The user buys <quantity> quantity of <fuelType> fuel
	And The user sends request to obtain details of previous order with order id <id>
	Then The user should see orders with fuel type <fuel> , order id <id> and quantity <quantity> were created before the current date
	Examples:
	| fuel     | id                                   | quantity |
	| gas      | 31fc32da-bccb-44ab-9352-4f43fc44ed4b | 5        |
	| electric | 080d9823-e874-4b5b-99ff-2021f2a59b25 | 23       |
	| nuclear  | 2cdd6f69-95df-437e-b4d3-e772472db8de | 15       |
	| oil      | 080d9823-e874-4b5b-99ff-2021f2a59b24 | 25       |
	| gas      | 31fc32da-bccb-44ab-9352-4f43fc44ed4b | 5        |
