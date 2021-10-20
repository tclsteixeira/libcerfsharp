## Libcerfsharp


This is the home page of **Libcerfsharp**, a C# port from C library [libcerf](https://jugit.fz-juelich.de/mlz/libcerf/-/blob/master/README.md) version 1.17. 
**libcerf** is a self-contained numeric library that provides an efficient and accurate implementation of complex error functions, along with Dawson, Faddeeva, and Voigt functions.
 

**Author Libcerfsharp:**
<i>Tiago C. Teixeira</i>
<i>Copyright (c) 2021 Tiago C. Teixeira</i>

**Link to original C library 'libcerf'**
https://jugit.fz-juelich.de/mlz/libcerf/-/blob/master/README.md


## User Documentation
### Synopsis

In the following, "Complex" stands for .Net System.Numerics [Complex](https://docs.microsoft.com/en-us/dotnet/api/system.numerics.complex?view=net-5.0) structure;

* Complex [cerf](https://apps.jcns.fz-juelich.de/man/cerf.html) (Complex): The complex error function erf(z).
* Complex [cerfc](https://apps.jcns.fz-juelich.de/man/cerf.html) (Complex): The complex complementary error function erfc(z) = 1 - erf(z).
* Complex [cerfcx](https://apps.jcns.fz-juelich.de/man/erfcx.html) (Complex z): The underflow-compensating function erfcx(z) = exp(z^2) erfc(z).
* double [erfcx](https://apps.jcns.fz-juelich.de/man/erfcx.html) (double x): The same for real x.
* Complex [cerfi](https://apps.jcns.fz-juelich.de/man/erfi.html) (Complex z): The imaginary error function erfi(z) = -i erf(iz).
* double [erfi](https://apps.jcns.fz-juelich.de/man/erfi.html) (double x): The same for real x.
* Complex [w_of_z](https://apps.jcns.fz-juelich.de/man/w_of_z.html) (Complex z): Faddeeva's scaled Complex error function w(z) = exp(-z^2) erfc(-iz).
* double [im_w_of_x](https://apps.jcns.fz-juelich.de/man/w_of_z.html) (double x): The same for real x, returning the purely imaginary result as a real number.
* Complex [cdawson](https://apps.jcns.fz-juelich.de/man/dawson.html) (complex z): Dawson's integral D(z) = sqrt(pi)/2 * exp(-z^2) * erfi(z).
* double [dawson](https://apps.jcns.fz-juelich.de/man/dawson.html) (double x): The same for real x.
* double [voigt](https://apps.jcns.fz-juelich.de/man/voigt.html) (double x, double sigma, double gamma): The convolution of a Gaussian and a Lorentzian.
* double [voigt_hwhm](https://apps.jcns.fz-juelich.de/man/voigt_hwhm.html) (double sigma, double gamma): The half width at half maximum of the Voigt profile.

## Accuracy

By construction, it is expected that the relative accuracy is generally better than 1E-13. This has been confirmed by comparison with high-precision Maple computations and with a long double computation using Fourier transform representation and double-exponential transform.

## Copyright and Citation from original C library

Copyright (C) *[Steven G. Johnson](http://math.mit.edu/~stevenj/)*, Massachusetts Institute of Technology, 2012; *[Joachim Wuttke](https://jugit.fz-juelich.de/j.wuttke)*, Forschungszentrum Jülich, 2013.

License: [MIT License](https://opensource.org/licenses/MIT)

When using libcerf in scientific work, please cite as follows:

- S. G. Johnson, J. Wuttke: libcerf, numeric library for complex error functions, version [...], https://jugit.fz-juelich.de/mlz/libcerf
If appropriate cite also the authors of language bindings or binary packages.

## Further references

Most function evaluations in this library rely on Faddeeva's function w(z).
This function has been reimplemented from scratch by [Steven G. Johnson](http://math.mit.edu/~stevenj/);
project web site http://ab-initio.mit.edu/Faddeeva. The implementation partly relies on algorithms from the following publications:

- Walter Gautschi, Efficient computation of the complex error function, SIAM J. Numer. Anal. 7, 187 (1970).
- G. P. M. Poppe and C. M. J. Wijers, More efficient computation of the complex error function, ACM Trans. Math. Soft. 16, 38 (1990).
- Mofreh R. Zaghloul and Ahmed N. Ali, Algorithm 916: Computing the Faddeyeva and Voigt Functions, ACM Trans. Math. Soft. 38, 15 (2011).

> Please report bugs to the package maintainer.

> Written with [StackEdit](https://stackedit.io/).
