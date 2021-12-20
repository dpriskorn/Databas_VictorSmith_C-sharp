Det här projekt är en studieuppgift i kursen Datamodellering på MIUN 2021.

Kursen hade till förmål att hjälpa oss jobba mot en postgresql databas i C# samt skriva en WPF GUI enligt uppsatta krav där användaren ska kunna ta del av datan i databasen på ett överskådligt sätt.

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
