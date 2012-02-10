#Up Check :: Simple Health Monitoring for ASP.NET Applications

---

This small app will attempt to connect to your databases and verify that your Windows Services are running.

---

## Usage

1. Add your database connection string to the `connectionStrings` section of the Web.config:

		<connectionStrings>

2. Configure logging to output to your logs directory. Be sure to give the IIS AppPool write permissions to your logs directory:

		<listeners>

3. Publish the up.ashx and Web.config files to an IIS Virtual Directory (default web.config is ASP.NET 4).

4. Setup an HTTP monitor (i.e. binarycanary.com) to hit the up.ashx periodically. A `200` response means that no exceptions where thrown. A `500` means an exception was encountered.




## Options

If debug is set to `true`, the 

		<system.web>