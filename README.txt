Det här projekt är en studieuppgift i kursen Datamodellering på MIUN 2021.

Projektet gjordes delvist i grupp men det mesta kod härinne har jag skrivit efter att gruppen upplöstes.

# Fix av postgresql
följ https://stackoverflow.com/a/21023029
kör följande från postgresql/13/bin mappen:
(i cmd)
psql.exe -U dbtest -p 5433 -d Klimatobservation -f C:\Users\$USER\Documents\truncate.sql
därefter
psql.exe -U dbtest -p 5433 -d Klimatobservation -f C:\Users\$USER\Documents\data.sql

klart om den inte klagar utan bara säger set, alter och copy :)
om du får några fel så skicka en bild på discord och ett sms till mig :)
