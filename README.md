#Up Check :: Simple Health Monitoring for Windows Servers
---

This small app will attempt to connect to your server's Windows Services and databases and verify that everything is running as expected.

---

## Usage

1. Add your remote service monitoring details to the `remoteServices` section of the Web.config:

		<remoteServices>		  <add name="SQLSERVERAGENT"        	path="\\server\root\CIMV2"	        username=".\user"    	    password="password"        	authority=""	        state="running"/>		  <add name="MSSQLSERVER"        	path="\\server\root\CIMV2"	        username=".\user"    	    password="password"        	authority=""	        state="running"/>		</remoteServices>
Use Start > Run > WBEMTEST to help test for valid path/username/password/authority settings.


2. Add your database connection string to the `connectionStrings` section of the Web.config:

		<connectionStrings>		  <clear/>		  <add name="ASPState"		       connectionString="Data Source=.;Initial Catalog=ASPState;User ID=sa;Pwd=tester12"			   providerName="System.Data.SqlClient"/>		</connectionStrings>

3. Configure logging to output to your logs directory. Be sure to give the IIS AppPool user account write permissions to your logs directory:

		<listeners>		  <add name="outfile"		       type="System.Diagnostics.TextWriterTraceListener"		       initializeData="c:\temp\upcheck.log"/>		</listeners>


4. If debug is set to `true`, the exceptions that are thrown will be the actual exceptions encountered. If set to `false`, the exception will be a generic `ERROR:` message suitable for public viewing:

		<system.web>		  <compilation debug="true"        		       targetFramework="4.0"/>		</system.web>

5. Build and publish the `bin/`, `up.ashx`, and `Web.config` files to an IIS Virtual Directory (default settings are for ASP.NET 4).

6. Setup an HTTP monitor (i.e. binarycanary.com) to hit `up.ashx` periodically. A `200` response means that no exceptions were thrown. A `500` means an exception was encountered. Monitor the trace log for more details.


