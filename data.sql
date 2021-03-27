SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;

-- fix column name
alter table country
rename column country to name;

--
-- TOC entry 2249 (class 0 OID 175246)
-- Dependencies: 191
-- Data for Name: country; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.country (name) FROM stdin;
Sverige
Norge
Finland
\.

--
-- TOC entry 2257 (class 0 OID 175270)
-- Dependencies: 199
-- Data for Name: observer; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.observer (firstname, lastname) FROM stdin;
Erik	Öberg
Johnny	Abrahamsson
\.


--
-- TOC entry 2259 (class 0 OID 175278)
-- Dependencies: 201
-- Data for Name: unit; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.unit (type, abbreviation) FROM stdin;
Meter per sekund	m/s
Grader celcius	° C
Centimeter	cm
Antal	st
\.


--
-- TOC entry 2245 (class 0 OID 175230)
-- Dependencies: 187
-- Data for Name: area; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.area (name, country_id) FROM stdin;
Frösön	1
Arholma-Idö	1
\.

COPY public.geolocation (latitude, longitude, area_id) FROM stdin;
63.2014459999999971	14.5258090000000006	1
49.2227572999999978	-122.617545100000001	2
\.


--
-- TOC entry 2247 (class 0 OID 175238)
-- Dependencies: 189
-- Data for Name: category; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.category (id, name, basecategory_id, unit_id) FROM stdin;
1	Djur	\N	\N
2	Väder	\N	\N
3	Träd	\N	\N
8	Istjocklek	2	4
9	Vindstyrka	2	2
4	Fjällräv	1	1
5	Hare	1	1
6	Vildsvin	1	1
12	Fjällripa	1	1
16	Snödjup	2	4
7	Lufttemperatur	2	3
17	Berguv	1	1
10	Vinterpäls	5	1
11	Sommarpäls	5	1
13	Vinterdräkt	12	1
14	Sommardräkt	12	1
15	Fjällbjörk	3	1
\.

COPY public.observation (date, observer_id, geolocation_id) FROM stdin;
2019-07-05	1	1
2020-02-23	2	2