# NEL.LibPostalNet

# Purpose

This is a nuget package for LibPostalNet, including data files and support for both windows and linux.



**Users of this package WILL need to reset the libpostal data directory at runtime.  See the [LibPostalNet](https://github.com/mapo80/LibPostalNet) console demonstration
program for how to do this. (snippet below)**

```
	libpostal.LibpostalSetupDatadir(dataPath);
	libpostal.LibpostalSetupParserDatadir(dataPath);
	libpostal.LibpostalSetupLanguageClassifierDatadir(dataPath);
```

# How it was built

## Windows
See the [LibPostal](https://github.com/openvenues/libpostal) project to set up the environment.
Then for the library build:

```
git clone https://github.com/openvenues/libpostal
cd libpostal
cp -rf windows/* ./
./bootstrap.sh
./configure --datadir=/c --disable-sse2
make -j4
make install
```

There were issues reported with SSE, so I disabled it.

## Linux

See the [LibPostal](https://github.com/openvenues/libpostal) project to set up the environment.
Then for the library build:

```
git clone https://github.com/openvenues/libpostal
cd libpostal
./bootstrap.sh
./configure --datadir=`pwd`/libpostal_data --disable-sse2
make -j4
make install
```