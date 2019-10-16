# [atet](https://github.com/atet) / [mobile_app_marketing](https://github.com/atet/mobile_app_marketing)

### Mobile Applications Marketing: Data Analytics on Shop Click

* **Update: Game released as "Bigly's Shop Click" on Google Play Store** (Early Access Open Beta, https://play.google.com/store/apps/details?id=com.atetkaolabs.shopclick)
* Goal: Apply my data science skill set on the mobile applications marketing field.
* There are four major parts to this project:
   * 1.) (DONE) Development of a mobile game
   * 2.) (DONE) Launch
   * 3.) (TODO) User acquisition
   * 4.) (TODO) Marketing dashboard

--------------------------------------------------------------------------------------------------

### Table of Contents

* [Game Design](#game-design)
* [Game Development](#game-development)
* [Android Developer Fee](#android-developer-fee)
* [Monetization](#monetization)
   * [Advertisements](#monetization-advertisements)
   * [In-App Purchases](#monetization-in-app-purchases)
* [Marketing Analytics Hooks](#marketing-analytics-hooks)
* [Pre-Launch Testing](#pre-launch-testing)
* [Media Kit](#media-kit)
* [Required Legal Stuff for Google Play Store](#required-legal-stuff-for-google-play-store)
* [Launch on Google Play Store](#launch-on-google-play-store)
* [Obtaining Users](#obtaining-users)
   * [Organic Growth](#organic-growth)
   * [Inorganic Growth](#inorganic-growth)
* [Customer Service](#customer-service)
* [Mobile App Marketing Primer](#mobile-app-marketing-primer)
* [Marketing Dashboard](#marketing-dashboard)
* [Acknowledgements](#acknowledgements)

--------------------------------------------------------------------------------------------------

### Game Design

* The purpose of my project is not making a AAA game; this is a means to the end for the marketing analytics part.
* Regardless, my game must be compelling _enough_ that a user would:
   * 1.) Play for more than a few minutes
   * 2.) Reach a conversion goal, e.g. watch an advertisement
* To bypass lengthy design and planning, I am replicating a genre that would be managable for me to solely develop: A 2D, menu-driven, clicker/incremental game.

[Back to Top](#table-of-contents)

--------------------------------------------------------------------------------------------------

### Game Development

* Engine: Unity 2019.2.0f1 (C#).
* Assets: CC0 (https://wiki.creativecommons.org/wiki/CC0) licensed assets from Open Game Art (https://opengameart.org/)
* Platform: Android

[Back to Top](#table-of-contents)

--------------------------------------------------------------------------------------------------

### Android Developer Fee

* There is a one-time $25 USD fee you need to pay to publish apps on the Google Play Store.
* Sign up/log in here: https://developer.android.com

[Back to Top](#table-of-contents)

--------------------------------------------------------------------------------------------------

### Monetization

#### Monetization: Advertisements

* **My conversion goal is that a user get all the way to watching at least one advertisement**.
* I am using Google AdMob to deliver rewarded video-based advertisements in exchange for in-game premium currency.

#### Monetization: In-App Purchases

* I plan to set up this framework in my app, but it is more of an academic exercise than a monetization goal.

[Back to Top](#table-of-contents)

--------------------------------------------------------------------------------------------------

### Marketing Analytics Hooks

* **Stay tuned**: Data from Google Play Console Statistics dashboard and Google AdMob Console may suffice instead of hard-coded hooks to report KPIs.
* There are a few KPI's that I am keeping track of:
   * Android device ID, this is unique to each device
   * Session length/count
   * Session time until conversion

[Back to Top](#table-of-contents)

--------------------------------------------------------------------------------------------------

### Pre-Launch Testing

#### Alpha Testing

* Specific implementations were privately tested on several Android devices.

#### Beta Testing

* Builds in which a user could make it to endgame (level 80) will be tested by a small group of experienced mobile gaming players.
* An open beta test is currently being run on Google Play Store Early Access.

[Back to Top](#table-of-contents)

--------------------------------------------------------------------------------------------------

### Media Kit

* Google Play Store link: https://play.google.com/store/apps/details?id=com.atetkaolabs.shopclick
* Website: http://labs.athitkao.com
* Graphics:

   #### Icon
   
   [![https://github.com/atet/mobile_app_marketing/blob/master/.img/icon_apk_64x64.png?raw=true](https://github.com/atet/mobile_app_marketing/blob/master/.img/icon_apk_64x64.png?raw=true)](#nolink)
   
   #### Feature Graphic

   [![https://github.com/atet/mobile_app_marketing/blob/master/.img/feature_256x125.jpg?raw=true](https://github.com/atet/mobile_app_marketing/blob/master/.img/feature_256x125.jpg?raw=true)](#nolink)

   #### Screenshots

   [![https://github.com/atet/mobile_app_marketing/blob/master/.img/screenshots_435x256.png?raw=true](https://github.com/atet/mobile_app_marketing/blob/master/.img/screenshots_435x256.png?raw=true)](#nolink)

[Back to Top](#table-of-contents)

--------------------------------------------------------------------------------------------------

### Required Legal Stuff for Google Play Store

***<center>I am not a lawyer and am not providing any legal advice here.</center>***

* In order to collect _any_ user data from an app on the Google Play Store, **you must provide a link to your user agreement that exists online**.
* It is your responsibility to protect any user information you collect for marketing analytics, etc.

[Back to Top](#table-of-contents)

--------------------------------------------------------------------------------------------------

### Launch on Google Play Store

* Even if you watch a current walkthrough video on Youtube, etc. many details can change between Unity and Google Play overnight.
* These are the big differences that I experienced:
   * You must build your *.apk OR *.aab as ARM64-only, select Scripting Backend as IL2CPP and the Target Architecture as only ARM64
   * You must increment the Bundle Version Code **everytime** you upload a build to Google Play for processing, even if there was an error with the build
   * You build cannot be in development mode or have any Debug.Log commands (that are uncommented)
   * Your first submission may take a few days to clear Google Play's checks

[Back to Top](#table-of-contents)

--------------------------------------------------------------------------------------------------

### Obtaining Users

#### Word-of-Mouth

#### Organic Growth

#### Inorganic Growth

[Back to Top](#table-of-contents)

--------------------------------------------------------------------------------------------------

### Customer Service

* If customer issues come up, you need to handle them appropriately before too many bad reviews sink your app.
* I will be handling issues on a case-by-case basis and personally responding to user reviews.

[Back to Top](#table-of-contents)

--------------------------------------------------------------------------------------------------

### Mobile App Marketing Primer

#### Introduction

* Every project will have different goals. These goals will define what marketing analytics are important to track as key performance indicators (KPIs).
* An example of a critical KPI that would guide high-level strategic decisions is user retention. This is an indicator of an app's success and is represented by _n_% user retention after _m_ days.

#### Terminology

* The following are popular terms in the mobile application marketing space:

Term | Description
--- | ---
KPI | Key Performance Indicator
DAU | Daily Active Users
ARPU | Average Revenue Per User
Session | Event where a user is actively using the application
Session Length | The amount of time a user spends during a session
Session Inverval | The amount of time between active sessions

#### Experimental Design

* Marketing and analytics are serious business; there are people that work on just these aspects of a mobile app full-time.
* From my experience in biotech analytics, be prepared for projects ranging from "We need to know _xyz_ by today" to multi-year sagas.
* This article provides an excellent overview of experimental design in analytics projects: https://campus.datacamp.com/courses/experimental-design-in-r/introduction-to-experimental-design?ex=1

[Back to Top](#table-of-contents)

--------------------------------------------------------------------------------------------------

### Marketing Dashboard

#### Under Construction

* https://scdash.athitkao.com
* Framework: Shiny (R Statistical Language)
* Webserver: Linux

This is the _coup de grace_ of my project. All the KPIs from my Shop Click app will be summarized here.

[Back to Top](#table-of-contents)

--------------------------------------------------------------------------------------------------

### Acknowledgements

* **Mondul Kao**: I wouldn't have started on this journey without him.
* **The Gaming Industry**: Everyone from Big Huge Games and GDC that were encouraging and welcoming.
* **GameDev.tv**: Thanks Ben, Rick, and Yann for making high-quality educational content on Udemy.
* **Countless Others**: From open game assets to helping with bugs, many thanks to the online community.

[Back to Top](#table-of-contents)

--------------------------------------------------------------------------------------------------
