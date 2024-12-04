# ModType Enumeration

## Definition
Namespace: OSLoaderCommons  
Assembly: OSloader.dll  
Source: ModType.cs  

## Description
Enum containing the possible values of the `info.json`'s `modType` property.

## Properties

Property | Description
-- | -
`(0) Standalone` | This mod does not have any dependency nor can it be used as a dependency.
`(1) Dependency` | This mod can **only** be used as a dependency.
`(2) RequiresDependency` | This mod requires a dependency.