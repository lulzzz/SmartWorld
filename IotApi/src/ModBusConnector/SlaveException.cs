﻿namespace Modbus
{
    using System;
    using System.Diagnostics.CodeAnalysis;


    using Message;

    /// <summary>
    ///     Represents slave errors that occur during communication.
    /// </summary>

    public class SlaveException : Exception
    {
        private const string SlaveAddressPropertyName = "SlaveAdress";
        private const string FunctionCodePropertyName = "FunctionCode";
        private const string SlaveExceptionCodePropertyName = "SlaveExceptionCode";

        private readonly SlaveExceptionResponse _slaveExceptionResponse;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SlaveException" /> class.
        /// </summary>
        public SlaveException()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="SlaveException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public SlaveException(string message)
            : base(message)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="SlaveException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public SlaveException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        internal SlaveException(SlaveExceptionResponse slaveExceptionResponse)
        {
            _slaveExceptionResponse = slaveExceptionResponse;
        }

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Used by test code.")]
        internal SlaveException(string message, SlaveExceptionResponse slaveExceptionResponse)
            : base(message)
        {
            _slaveExceptionResponse = slaveExceptionResponse;
        }



        /// <summary>
        ///     Gets a message that describes the current exception.
        /// </summary>
        /// <value>
        ///     The error message that explains the reason for the exception, or an empty string.
        /// </value>
        public override string Message
        {
            get
            {
                string responseString;
                responseString = _slaveExceptionResponse != null ? string.Concat(Environment.NewLine, _slaveExceptionResponse) : string.Empty;
                return string.Concat(base.Message, responseString);
            }
        }

        /// <summary>
        ///     Gets the response function code that caused the exception to occur, or 0.
        /// </summary>
        /// <value>The function code.</value>
        public byte FunctionCode
        {
            get { return _slaveExceptionResponse != null ? _slaveExceptionResponse.FunctionCode : (byte)0; }
        }

        /// <summary>
        ///     Gets the slave exception code, or 0.
        /// </summary>
        /// <value>The slave exception code.</value>
        public byte SlaveExceptionCode
        {
            get { return _slaveExceptionResponse != null ? _slaveExceptionResponse.SlaveExceptionCode : (byte)0; }
        }

        /// <summary>
        ///     Gets the slave address, or 0.
        /// </summary>
        /// <value>The slave address.</value>
        public byte SlaveAddress
        {
            get { return _slaveExceptionResponse != null ? _slaveExceptionResponse.SlaveAddress : (byte)0; }
        }

        /// <summary>
        ///     When overridden in a derived class, sets the <see cref="T:System.Runtime.Serialization.SerializationInfo"></see>
        ///     with information about the exception.
        /// </summary>
        /// <param name="info">
        ///     The <see cref="T:System.Runtime.Serialization.SerializationInfo"></see> that holds the serialized
        ///     object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        ///     The <see cref="T:System.Runtime.Serialization.StreamingContext"></see> that contains contextual
        ///     information about the source or destination.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">The info parameter is a null reference (Nothing in Visual Basic). </exception>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="SerializationFormatter" />
        /// </PermissionSet>

    }
}
