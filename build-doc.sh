#!/bin/bash

exdir=$(dirname `readlink -f "$0"`)

dotnet build /p:CopyLocalLockFileAssemblies=true
docpal --proptable --mgtable --mgspace -out ./tmp-api-doc "$exdir"/netcore-csv/bin/Debug/netstandard2.0/netcore-csv.dll

rm -fr "$exdir"/doc/api
mkdir -p "$exdir"/doc/api
mv "$exdir"/tmp-api-doc/docs/SearchAThing/* "$exdir"/doc/api
rm -fr "$exdir"/tmp-api-doc
