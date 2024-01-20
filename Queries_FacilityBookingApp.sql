/*MOST POPULAR PRODUCT*/
SELECT TOP 3
f.Name,
	COUNT(b.FacilityId) AS 'BookingCounts' 
FROM Bookings b
JOIN Facilities f ON b.FacilityId = f.Id
GROUP BY f.Name
ORDER BY 'BookingCounts' DESC

/*MOST LOYAL CUSTOMER*/
SELECT TOP 5
CONCAT(c.FirstName, ' ', c.LastName) AS Customer,
COUNT(b.CustomerId) AS 'BookingCount'
FROM Bookings b
JOIN Customers c ON b.CustomerId = c.Id
GROUP BY 
c.FirstName,
c.LastName
ORDER BY BookingCount DESC

/*PERCENTAGE OF FACILITIES BOOKED*/
SELECT
    Ceiling((COUNT(CASE WHEN fs.AvailabilityStatus <> 'Available' THEN 1 END) * 100.0) / COUNT(*)) AS PercentageBooked
FROM	FacilitySchedules fs

/*PERCENTAGE OF DACILITIES BOOKED WITH CAPACITY OF AT LEAST 50*/
SELECT 
     f.Name,
    COUNT(*) AS BookingCount,
	format(ROUND((COUNT(*) * 100.0) / (SELECT COUNT(*) FROM Bookings), 2),'N') AS PercentageBooked
FROM 
    Bookings b
JOIN 
    FacilitySchedules fs ON b.FacilityScheduleId = fs.Id
JOIN 
    Facilities f ON b.FacilityId = f.Id
	WHERE f.Capacity>=50
	GROUP BY	f.Name
