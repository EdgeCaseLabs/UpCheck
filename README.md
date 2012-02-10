#Up Check :: Simple Health Monitoring for ASP.NET Applications

---

This small app will attempt to connect to your databases and verify that your Windows Services are running.

---

## Usage

1. Add your database connection string to the `connectionStrings` section of the Web.config:

		<connectionStrings>		  <clear/>		  <add name="ASPState"		       connectionString="Data Source=.;Initial Catalog=ASPState;User ID=sa;Pwd=tester12"			   providerName="System.Data.SqlClient"/>		</connectionStrings>

2. Configure logging to output to your logs directory. Be sure to give the IIS AppPool write permissions to your logs directory:

		<listeners>		  <add name="outfile"		       type="System.Diagnostics.TextWriterTraceListener"		       initializeData="c:\temp\upcheck.log"/>		</listeners>


3. If debug is set to `true`, the exceptions that are thrown will be the actual exceptions encountered. If set to `false`, the exception will be a generic `ERROR: …` message.

		<system.web>		  <compilation debug="true"        		       targetFramework="4.0"/>		</system.web>

4. Publish the up.ashx and Web.config files to an IIS Virtual Directory (default web.config is ASP.NET 4).

5. Setup an HTTP monitor (i.e. binarycanary.com) to hit the up.ashx periodically. A `200` response means that no exceptions where thrown. A `500` means an exception was encountered.


