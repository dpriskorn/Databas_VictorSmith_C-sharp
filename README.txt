följ https://stackoverflow.com/a/21023029
kör följande från postgresql/13/bin mappen:
(i cmd)
psql.exe -U dbtest -p 5433 -d Klimatobservation -f C:\Users\fuck-\Documents\truncate.sql
därefter
psql.exe -U dbtest -p 5433 -d Klimatobservation -f C:\Users\fuck-\Documents\data.sql

klart om den inte klagar utan bara säger set, alter och copy :)
om du får några fel så skicka en bild på discord och ett sms till mig :)