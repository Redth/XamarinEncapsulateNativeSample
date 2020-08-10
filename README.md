# Xamarin Encapsulate Native Sample

## The problem
Binding native SDK's for use with Xamarin iOS and Android can sometimes be difficult, especially when there are large API surfaces with complex types, inheritance trees, and generics.  Developers often end up trying to bind the entire API surface of a native SDK only to use a small subset of those API's in their app.

## Simplifying bindings
Native SDK's with very simple API's of simple types, or types that are already bound in the core Xamarin SDK's are very easy to bind and require very little to no intervention in the binding process.

While we can and should continue to improve the binding tools, we can also turn the problem on its head a bit.

If we create a simple 'wrapper' API written in Swift (iOS) / Java (Android) to encapsulate the functionality we need from the API we can create a much more simplified API surface which the binding tools will almost always be able to properly bind without interventions.

## Adding wrapper automations

For the cases where this wrapper pattern proves useful, it's entirely possible for us to facilitate the process of creating boilerplate native Swift and Java projects, building them, and generating bindings for them such that developers wanting to leverage this pattern have very little overhead in its execution.  

Xamarin projects could link a native XCode or Android Studio project in their .csproj files which we could then build and bind dynamically as part of our build process.

# Sample

This repository contains a sample leveraging the MapBox SDK which is notoriously difficult to bind for Xamarin.  It demonstrates creating native Swift and Java wrapper api's which can then be compiled and used in binding projects also in this repository.  The wrapper API's are used in Xamarin.Forms renderer implementations for each platform, providing a Xamarin.Forms shared code `MapboxView` control with an `AddMarker(..)` API as well as events for when markers are tapped.  This demonstrates a an API wrapper pattern for bi-directional communication, and marshalling of complex native objects in their more simplified base types (eg: `MGLMapboxView` is simply a `UIView` in the wrapper API).

## Other Considerations
There are several things this sample does not currently do:

1. It does not help with generating native wrapper API projects or building them (other than the build.ps1 script)
2. It does not help with resolving dependency chains (a sometimes complex problem on Android).
3. It does not help with automating the binding process - on Android this is currently possible, on iOS we'll need a more complete 'Binding Tools for Swift' to replace the `@objc` header generation we currently use to then bind with traditional iOS binding techniques.

# The end goal

If this is a useful pattern, I'd like to explore streamlining the developer experience such that it's trivial to implement this pattern from generating native projects, building them, creating binaries, assisting with dependency resolution and consumption, and automating the binding process.
